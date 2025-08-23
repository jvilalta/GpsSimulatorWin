using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsTimer = System.Windows.Forms.Timer;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

namespace GpsSimulator
{
    public partial class MainForm : Form
    {
        private GpsSimulatorEngine _gpsEngine;
        private WinFormsTimer _displayTimer;
        private NmeaNetworkService _networkService;
        
        // UI Controls
        private Label _statusLabel;
        private Label _latitudeLabel;
        private Label _longitudeLabel;
        private Label _speedLabel;
        private Label _headingLabel;
        private Label _altitudeLabel;
        private Label _satellitesLabel;
        private Label _nmeaLabel;
        private Label _networkStatusLabel;
        
        private TextBox _latitudeTextBox;
        private TextBox _longitudeTextBox;
        private TextBox _speedTextBox;
        private TextBox _nmeaOutput;
        private TextBox _tcpPortTextBox;
        private TextBox _udpPortTextBox;
        
        // New degree/minute input fields
        private TextBox _latitudeDegreesTextBox;
        private TextBox _latitudeMinutesTextBox;
        private TextBox _longitudeDegreesTextBox;
        private TextBox _longitudeMinutesTextBox;
        private ComboBox _latitudeDirectionComboBox;
        private ComboBox _longitudeDirectionComboBox;
        
        private Button _startButton;
        private Button _stopButton;
        private Button _setPositionButton;
        private Button _addWaypointButton;
        private Button _clearWaypointsButton;
        private Button _exportGpxButton;
        private Button _startNetworkButton;
        private Button _stopNetworkButton;
        
        private CheckBox _enableTcpCheckBox;
        private CheckBox _enableUdpCheckBox;
        
        private ListBox _waypointsListBox;
        private ListBox _networkLogListBox;
        
        public MainForm()
        {
            InitializeComponent();
            InitializeGpsSimulator();
        }
        
        private void InitializeGpsSimulator()
        {
            _gpsEngine = new GpsSimulatorEngine();
            _gpsEngine.PositionUpdated += OnPositionUpdated;
            
            _networkService = new NmeaNetworkService();
            _networkService.StatusChanged += OnNetworkStatusChanged;
            _networkService.ErrorOccurred += OnNetworkError;
            
            _displayTimer = new WinFormsTimer();
            _displayTimer.Interval = 500; // Update display every 500ms
            _displayTimer.Tick += OnDisplayTimerTick;
            _displayTimer.Start();
            
            UpdateDisplay();
        }
        
        private void OnPositionUpdated(object? sender, GpsData gpsData)
        {
            // This will be called from the timer thread, so we need to invoke on UI thread
            if (InvokeRequired)
            {
                Invoke(new Action<object?, GpsData>(OnPositionUpdated), sender, gpsData);
                return;
            }
            
            // Update will happen in the display timer
        }
        
        private void OnDisplayTimerTick(object? sender, EventArgs e)
        {
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            if (_gpsEngine?.CurrentPosition == null) return;
            
            var pos = _gpsEngine.CurrentPosition;
            
            _statusLabel.Text = _gpsEngine.IsRunning ? "Running" : "Stopped";
            _statusLabel.ForeColor = _gpsEngine.IsRunning ? Color.Green : Color.Red;
            
            _latitudeLabel.Text = $"Latitude: {pos.Latitude:F6}°";
            _longitudeLabel.Text = $"Longitude: {pos.Longitude:F6}°";
            _speedLabel.Text = $"Speed: {pos.Speed:F1} knots";
            _headingLabel.Text = $"Heading: {pos.Heading:F1}°";
            _altitudeLabel.Text = $"Altitude: {pos.Altitude:F0} m";
            _satellitesLabel.Text = $"Satellites: {pos.Satellites}";
            
            var nmeaSentence = pos.ToNmeaString();
            _nmeaOutput.Text = nmeaSentence;
            
            // Update coordinate input fields if GPS is running (to show current position)
            if (_gpsEngine.IsRunning)
            {
                UpdateCoordinateInputFields();
            }
            
            // Transmit NMEA data over network if service is running
            if (_networkService.IsRunning)
            {
                _ = Task.Run(async () => await _networkService.BroadcastNmeaAsync(nmeaSentence));
            }
            
            // Update network status
            var networkStatus = _networkService.IsRunning ? "Network: Online" : "Network: Offline";
            if (_networkService.IsRunning)
            {
                var protocols = new List<string>();
                if (_networkService.IsTcpEnabled) protocols.Add($"TCP:{_networkService.TcpPort}");
                if (_networkService.IsUdpEnabled) protocols.Add($"UDP:{_networkService.UdpPort}");
                networkStatus += $" ({string.Join(", ", protocols)})";
            }
            _networkStatusLabel.Text = networkStatus;
        }
        
        private void StartButton_Click(object? sender, EventArgs e)
        {
            _gpsEngine.Start();
            _startButton.Enabled = false;
            _stopButton.Enabled = true;
        }
        
        private void StopButton_Click(object? sender, EventArgs e)
        {
            _gpsEngine.Stop();
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
        }
        
        private void SetPositionButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var lat = GetLatitudeFromInput();
                var lon = GetLongitudeFromInput();
                
                // Validate coordinates
                if (Math.Abs(lat) > 90)
                {
                    MessageBox.Show("Latitude must be between -90° and +90°.", "Invalid Latitude", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (Math.Abs(lon) > 180)
                {
                    MessageBox.Show("Longitude must be between -180° and +180°.", "Invalid Longitude", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                _gpsEngine.SetPosition(lat, lon);
                UpdateDisplay();
            }
            catch
            {
                MessageBox.Show("Please enter valid latitude and longitude values in degrees and decimal minutes.", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void AddWaypointButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var lat = GetLatitudeFromInput();
                var lon = GetLongitudeFromInput();
                
                // Validate coordinates
                if (Math.Abs(lat) > 90 || Math.Abs(lon) > 180)
                {
                    MessageBox.Show("Please enter valid coordinate values (Latitude: ±90°, Longitude: ±180°).", "Invalid Input", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                _gpsEngine.AddWaypoint(lat, lon);
                
                // Display in degrees/minutes format in the waypoints list
                var (latDeg, latMin) = DecimalDegreesToDegreesMinutes(lat);
                var (lonDeg, lonMin) = DecimalDegreesToDegreesMinutes(lon);
                var latDir = lat >= 0 ? "N" : "S";
                var lonDir = lon >= 0 ? "E" : "W";
                
                _waypointsListBox.Items.Add($"{latDeg}° {latMin:F2}' {latDir}, {lonDeg}° {lonMin:F2}' {lonDir}");
            }
            catch
            {
                MessageBox.Show("Please enter valid latitude and longitude values in degrees and decimal minutes.", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void ClearWaypointsButton_Click(object? sender, EventArgs e)
        {
            _gpsEngine.ClearWaypoints();
            _waypointsListBox.Items.Clear();
        }
        
        private void ExportGpxButton_Click(object? sender, EventArgs e)
        {
            if (_gpsEngine.Waypoints.Count == 0)
            {
                MessageBox.Show("No waypoints to export. Please add some waypoints first.", "No Waypoints", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "GPX files (*.gpx)|*.gpx|All files (*.*)|*.*";
                saveFileDialog.Title = "Export Waypoints as GPX";
                saveFileDialog.DefaultExt = "gpx";
                saveFileDialog.FileName = $"waypoints_{DateTime.Now:yyyyMMdd_HHmmss}.gpx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var gpxContent = GenerateGpxContent();
                        File.WriteAllText(saveFileDialog.FileName, gpxContent);
                        MessageBox.Show($"GPX file exported successfully to:\n{saveFileDialog.FileName}", "Export Successful", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to export GPX file:\n{ex.Message}", "Export Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private string GenerateGpxContent()
        {
            var sb = new StringBuilder();
            
            // GPX header
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<gpx version=\"1.1\" creator=\"GPS Simulator\" xmlns=\"http://www.topografix.com/GPX/1/1\">");
            
            // Metadata
            sb.AppendLine("  <metadata>");
            sb.AppendLine($"    <name>GPS Simulator Waypoints</name>");
            sb.AppendLine($"    <desc>Waypoints exported from GPS Simulator on {DateTime.Now:yyyy-MM-dd HH:mm:ss}</desc>");
            sb.AppendLine($"    <time>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}</time>");
            sb.AppendLine("  </metadata>");
            
            // Waypoints
            for (int i = 0; i < _gpsEngine.Waypoints.Count; i++)
            {
                var waypoint = _gpsEngine.Waypoints[i];
                sb.AppendLine($"  <wpt lat=\"{waypoint.lat:F6}\" lon=\"{waypoint.lon:F6}\">");
                sb.AppendLine($"    <name>Waypoint {i + 1}</name>");
                sb.AppendLine($"    <desc>Generated waypoint {i + 1}</desc>");
                sb.AppendLine("  </wpt>");
            }
            
            // Track (route through all waypoints)
            if (_gpsEngine.Waypoints.Count > 1)
            {
                sb.AppendLine("  <trk>");
                sb.AppendLine("    <name>GPS Simulator Route</name>");
                sb.AppendLine("    <desc>Route through all waypoints</desc>");
                sb.AppendLine("    <trkseg>");
                
                foreach (var waypoint in _gpsEngine.Waypoints)
                {
                    sb.AppendLine($"      <trkpt lat=\"{waypoint.lat:F6}\" lon=\"{waypoint.lon:F6}\">");
                    sb.AppendLine($"        <time>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}</time>");
                    sb.AppendLine("      </trkpt>");
                }
                
                sb.AppendLine("    </trkseg>");
                sb.AppendLine("  </trk>");
            }
            
            sb.AppendLine("</gpx>");
            
            return sb.ToString();
        }
        
        private void SpeedTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (double.TryParse(_speedTextBox.Text, out double speed))
            {
                _gpsEngine.SetSpeed(speed);
            }
        }
        
        private async void StartNetworkButton_Click(object? sender, EventArgs e)
        {
            try
            {
                // Configure network service
                if (int.TryParse(_tcpPortTextBox.Text, out int tcpPort))
                {
                    _networkService.ConfigureTcp(tcpPort, _enableTcpCheckBox.Checked);
                }
                
                if (int.TryParse(_udpPortTextBox.Text, out int udpPort))
                {
                    _networkService.ConfigureUdp(udpPort, _enableUdpCheckBox.Checked);
                }
                
                await _networkService.StartAsync();
                
                _startNetworkButton.Enabled = false;
                _stopNetworkButton.Enabled = true;
                _enableTcpCheckBox.Enabled = false;
                _enableUdpCheckBox.Enabled = false;
                _tcpPortTextBox.Enabled = false;
                _udpPortTextBox.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start network service: {ex.Message}", "Network Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void StopNetworkButton_Click(object? sender, EventArgs e)
        {
            _networkService.Stop();
            
            _startNetworkButton.Enabled = true;
            _stopNetworkButton.Enabled = false;
            _enableTcpCheckBox.Enabled = true;
            _enableUdpCheckBox.Enabled = true;
            _tcpPortTextBox.Enabled = true;
            _udpPortTextBox.Enabled = true;
        }
        
        private void OnNetworkStatusChanged(object? sender, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, string>(OnNetworkStatusChanged), sender, message);
                return;
            }
            
            _networkLogListBox.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");
            if (_networkLogListBox.Items.Count > 100)
            {
                _networkLogListBox.Items.RemoveAt(0);
            }
            _networkLogListBox.TopIndex = _networkLogListBox.Items.Count - 1;
        }
        
        private void OnNetworkError(object? sender, Exception ex)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, Exception>(OnNetworkError), sender, ex);
                return;
            }
            
            _networkLogListBox.Items.Add($"{DateTime.Now:HH:mm:ss} - ERROR: {ex.Message}");
            if (_networkLogListBox.Items.Count > 100)
            {
                _networkLogListBox.Items.RemoveAt(0);
            }
            _networkLogListBox.TopIndex = _networkLogListBox.Items.Count - 1;
        }
        
        /// <summary>
        /// Converts decimal degrees to degrees and decimal minutes
        /// </summary>
        private (int degrees, double minutes) DecimalDegreesToDegreesMinutes(double decimalDegrees)
        {
            var absoluteValue = Math.Abs(decimalDegrees);
            var degrees = (int)absoluteValue;
            var minutes = (absoluteValue - degrees) * 60.0;
            return (degrees, minutes);
        }
        
        /// <summary>
        /// Converts degrees and decimal minutes to decimal degrees
        /// </summary>
        private double DegreesMinutesToDecimalDegrees(int degrees, double minutes, bool isNegative)
        {
            var decimalDegrees = degrees + (minutes / 60.0);
            return isNegative ? -decimalDegrees : decimalDegrees;
        }
        
        /// <summary>
        /// Gets the current latitude from the degree/minute input fields
        /// </summary>
        private double GetLatitudeFromInput()
        {
            if (int.TryParse(_latitudeDegreesTextBox.Text, out int latDegrees) &&
                double.TryParse(_latitudeMinutesTextBox.Text, out double latMinutes))
            {
                var isNegative = _latitudeDirectionComboBox.SelectedItem?.ToString() == "S";
                return DegreesMinutesToDecimalDegrees(latDegrees, latMinutes, isNegative);
            }
            return 0.0;
        }
        
        /// <summary>
        /// Gets the current longitude from the degree/minute input fields
        /// </summary>
        private double GetLongitudeFromInput()
        {
            if (int.TryParse(_longitudeDegreesTextBox.Text, out int lonDegrees) &&
                double.TryParse(_longitudeMinutesTextBox.Text, out double lonMinutes))
            {
                var isNegative = _longitudeDirectionComboBox.SelectedItem?.ToString() == "W";
                return DegreesMinutesToDecimalDegrees(lonDegrees, lonMinutes, isNegative);
            }
            return 0.0;
        }
        
        /// <summary>
        /// Updates the degree/minute input fields with the current GPS position
        /// </summary>
        private void UpdateCoordinateInputFields()
        {
            if (_gpsEngine?.CurrentPosition == null) return;
            
            var pos = _gpsEngine.CurrentPosition;
            
            // Update latitude fields
            var (latDeg, latMin) = DecimalDegreesToDegreesMinutes(pos.Latitude);
            _latitudeDegreesTextBox.Text = latDeg.ToString();
            _latitudeMinutesTextBox.Text = latMin.ToString("F4");
            _latitudeDirectionComboBox.SelectedItem = pos.Latitude >= 0 ? "N" : "S";
            
            // Update longitude fields
            var (lonDeg, lonMin) = DecimalDegreesToDegreesMinutes(pos.Longitude);
            _longitudeDegreesTextBox.Text = lonDeg.ToString();
            _longitudeMinutesTextBox.Text = lonMin.ToString("F4");
            _longitudeDirectionComboBox.SelectedItem = pos.Longitude >= 0 ? "E" : "W";
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _displayTimer?.Dispose();
                _gpsEngine?.Dispose();
                _networkService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
