using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GpsSimulator
{
    /// <summary>
    /// Handles network transmission of NMEA data over TCP and UDP
    /// </summary>
    public class NmeaNetworkService : IDisposable
    {
        private TcpListener? _tcpListener;
        private UdpClient? _udpClient;
        private readonly List<NetworkStream> _tcpClients;
        private bool _isRunning;
        private CancellationTokenSource? _cancellationTokenSource;
        
        public int TcpPort { get; private set; }
        public int UdpPort { get; private set; }
        public bool IsTcpEnabled { get; private set; }
        public bool IsUdpEnabled { get; private set; }
        public bool IsRunning => _isRunning;
        
        public event EventHandler<string>? StatusChanged;
        public event EventHandler<Exception>? ErrorOccurred;
        
        public NmeaNetworkService()
        {
            _tcpClients = new List<NetworkStream>();
        }
        
        /// <summary>
        /// Configure TCP server settings
        /// </summary>
        public void ConfigureTcp(int port, bool enabled)
        {
            if (_isRunning)
                throw new InvalidOperationException("Cannot configure while service is running");
                
            TcpPort = port;
            IsTcpEnabled = enabled;
        }
        
        /// <summary>
        /// Configure UDP broadcast settings
        /// </summary>
        public void ConfigureUdp(int port, bool enabled)
        {
            if (_isRunning)
                throw new InvalidOperationException("Cannot configure while service is running");
                
            UdpPort = port;
            IsUdpEnabled = enabled;
        }
        
        /// <summary>
        /// Start the network services
        /// </summary>
        public async Task StartAsync()
        {
            if (_isRunning) return;
            
            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = true;
            
            try
            {
                if (IsTcpEnabled)
                {
                    await StartTcpServerAsync();
                }
                
                if (IsUdpEnabled)
                {
                    StartUdpBroadcast();
                }
                
                StatusChanged?.Invoke(this, "Network services started");
            }
            catch (Exception ex)
            {
                _isRunning = false;
                ErrorOccurred?.Invoke(this, ex);
                throw;
            }
        }
        
        /// <summary>
        /// Stop the network services
        /// </summary>
        public void Stop()
        {
            if (!_isRunning) return;
            
            _isRunning = false;
            _cancellationTokenSource?.Cancel();
            
            // Stop TCP server
            _tcpListener?.Stop();
            
            // Disconnect all TCP clients
            lock (_tcpClients)
            {
                foreach (var client in _tcpClients)
                {
                    try { client.Close(); } catch { }
                }
                _tcpClients.Clear();
            }
            
            // Stop UDP client
            _udpClient?.Close();
            _udpClient?.Dispose();
            _udpClient = null;
            
            StatusChanged?.Invoke(this, "Network services stopped");
        }
        
        /// <summary>
        /// Broadcast NMEA sentence to all connected clients
        /// </summary>
        public async Task BroadcastNmeaAsync(string nmeaSentence)
        {
            if (!_isRunning) return;
            
            var data = Encoding.ASCII.GetBytes(nmeaSentence + "\r\n");
            
            // Send to TCP clients
            if (IsTcpEnabled)
            {
                await BroadcastToTcpClientsAsync(data);
            }
            
            // Send via UDP broadcast
            if (IsUdpEnabled && _udpClient != null)
            {
                try
                {
                    await _udpClient.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Broadcast, UdpPort));
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(this, ex);
                }
            }
        }
        
        private async Task StartTcpServerAsync()
        {
            _tcpListener = new TcpListener(IPAddress.Any, TcpPort);
            _tcpListener.Start();
            
            // Accept clients in background
            _ = Task.Run(async () =>
            {
                while (_isRunning && !_cancellationTokenSource!.Token.IsCancellationRequested)
                {
                    try
                    {
                        var tcpClient = await _tcpListener.AcceptTcpClientAsync();
                        var stream = tcpClient.GetStream();
                        
                        lock (_tcpClients)
                        {
                            _tcpClients.Add(stream);
                        }
                        
                        StatusChanged?.Invoke(this, $"TCP client connected from {tcpClient.Client.RemoteEndPoint}");
                        
                        // Monitor client disconnect
                        _ = Task.Run(() => MonitorTcpClient(stream, tcpClient));
                    }
                    catch (ObjectDisposedException)
                    {
                        // Normal when stopping
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (_isRunning)
                            ErrorOccurred?.Invoke(this, ex);
                    }
                }
            }, _cancellationTokenSource.Token);
        }
        
        private void StartUdpBroadcast()
        {
            _udpClient = new UdpClient();
            _udpClient.EnableBroadcast = true;
        }
        
        private async Task BroadcastToTcpClientsAsync(byte[] data)
        {
            List<NetworkStream> clientsToRemove = new();
            
            lock (_tcpClients)
            {
                foreach (var client in _tcpClients)
                {
                    try
                    {
                        if (client.CanWrite)
                        {
                            _ = Task.Run(async () =>
                            {
                                try
                                {
                                    await client.WriteAsync(data, 0, data.Length);
                                    await client.FlushAsync();
                                }
                                catch
                                {
                                    // Client disconnected, will be removed by monitor
                                }
                            });
                        }
                        else
                        {
                            clientsToRemove.Add(client);
                        }
                    }
                    catch
                    {
                        clientsToRemove.Add(client);
                    }
                }
                
                foreach (var client in clientsToRemove)
                {
                    _tcpClients.Remove(client);
                    try { client.Close(); } catch { }
                }
            }
        }
        
        private void MonitorTcpClient(NetworkStream stream, TcpClient tcpClient)
        {
            try
            {
                // Wait for client to disconnect
                var buffer = new byte[1];
                while (stream.CanRead && tcpClient.Connected)
                {
                    try
                    {
                        var bytesRead = stream.Read(buffer, 0, 1);
                        if (bytesRead == 0)
                            break; // Client disconnected
                    }
                    catch
                    {
                        break; // Connection error
                    }
                    
                    Thread.Sleep(100);
                }
            }
            catch { }
            finally
            {
                // Remove client from list
                lock (_tcpClients)
                {
                    _tcpClients.Remove(stream);
                }
                
                try
                {
                    stream.Close();
                    tcpClient.Close();
                }
                catch { }
                
                StatusChanged?.Invoke(this, "TCP client disconnected");
            }
        }
        
        public void Dispose()
        {
            Stop();
            _cancellationTokenSource?.Dispose();
        }
    }
}
