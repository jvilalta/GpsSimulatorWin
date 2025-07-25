using System;
using System.Collections.Generic;
using Timer = System.Timers.Timer;
using System.Timers;

namespace GpsSimulator
{
    /// <summary>
    /// GPS simulation engine that generates realistic GPS data movement
    /// </summary>
    public class GpsSimulatorEngine : IDisposable
    {
        private readonly Timer _updateTimer;
        private GpsData _currentPosition;
        private double _targetLatitude;
        private double _targetLongitude;
        private double _baseSpeed;
        private bool _isRunning;
        private readonly Random _random;
        private readonly List<(double lat, double lon)> _waypoints;
        private int _currentWaypointIndex;
        
        public event EventHandler<GpsData>? PositionUpdated;
        
        public GpsData CurrentPosition => _currentPosition;
        public bool IsRunning => _isRunning;
        public double UpdateInterval { get; set; } = 1000; // ms
        
        public GpsSimulatorEngine()
        {
            _random = new Random();
            _updateTimer = new Timer();
            _updateTimer.Elapsed += OnTimerElapsed;
            _waypoints = new List<(double, double)>();
            
            // Initialize with default position (San Francisco area)
            _currentPosition = new GpsData
            {
                Latitude = 37.7749,
                Longitude = -122.4194,
                Speed = 0,
                Heading = 0,
                Altitude = 50
            };
            
            _targetLatitude = _currentPosition.Latitude;
            _targetLongitude = _currentPosition.Longitude;
            _baseSpeed = 30; // knots
        }
        
        public void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _updateTimer.Interval = UpdateInterval;
                _updateTimer.Start();
            }
        }
        
        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _updateTimer.Stop();
                _currentPosition.Speed = 0;
            }
        }
        
        public void SetPosition(double latitude, double longitude)
        {
            _currentPosition.Latitude = latitude;
            _currentPosition.Longitude = longitude;
            _targetLatitude = latitude;
            _targetLongitude = longitude;
        }
        
        public void SetTarget(double latitude, double longitude)
        {
            _targetLatitude = latitude;
            _targetLongitude = longitude;
        }
        
        public void SetSpeed(double speedKnots)
        {
            _baseSpeed = Math.Max(0, speedKnots);
        }
        
        public void AddWaypoint(double latitude, double longitude)
        {
            _waypoints.Add((latitude, longitude));
        }
        
        public void ClearWaypoints()
        {
            _waypoints.Clear();
            _currentWaypointIndex = 0;
        }
        
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (!_isRunning) return;
            
            UpdatePosition();
            _currentPosition.Timestamp = DateTime.UtcNow;
            
            PositionUpdated?.Invoke(this, _currentPosition);
        }
        
        private void UpdatePosition()
        {
            // If we have waypoints, navigate to them
            if (_waypoints.Count > 0)
            {
                var waypoint = _waypoints[_currentWaypointIndex];
                _targetLatitude = waypoint.lat;
                _targetLongitude = waypoint.lon;
                
                // Check if we've reached the current waypoint
                var distance = CalculateDistance(_currentPosition.Latitude, _currentPosition.Longitude,
                    _targetLatitude, _targetLongitude);
                
                if (distance < 0.001) // Very close to waypoint (roughly 100m)
                {
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
                }
            }
            
            // Calculate distance to target
            var distanceToTarget = CalculateDistance(_currentPosition.Latitude, _currentPosition.Longitude,
                _targetLatitude, _targetLongitude);
            
            if (distanceToTarget > 0.0001) // If not at target
            {
                // Calculate bearing to target
                var bearing = CalculateBearing(_currentPosition.Latitude, _currentPosition.Longitude,
                    _targetLatitude, _targetLongitude);
                
                // Update heading (with some smoothing)
                var headingDiff = bearing - _currentPosition.Heading;
                if (headingDiff > 180) headingDiff -= 360;
                if (headingDiff < -180) headingDiff += 360;
                
                _currentPosition.Heading += headingDiff * 0.1; // Smooth turning
                if (_currentPosition.Heading < 0) _currentPosition.Heading += 360;
                if (_currentPosition.Heading >= 360) _currentPosition.Heading -= 360;
                
                // Calculate speed (slow down when approaching target)
                var speedFactor = Math.Min(1.0, distanceToTarget * 1000); // Slow down within 1km
                _currentPosition.Speed = _baseSpeed * speedFactor;
                
                // Add some random variation
                _currentPosition.Speed += (_random.NextDouble() - 0.5) * 2;
                _currentPosition.Speed = Math.Max(0, _currentPosition.Speed);
                
                // Move towards target
                var speedMs = _currentPosition.Speed * 0.514444; // Convert knots to m/s
                var distancePerUpdate = speedMs * (UpdateInterval / 1000.0);
                var distanceDegrees = distancePerUpdate / 111320.0; // Rough meters to degrees conversion
                
                var deltaLat = distanceDegrees * Math.Cos(ToRadians(bearing));
                var deltaLon = distanceDegrees * Math.Sin(ToRadians(bearing)) / 
                              Math.Cos(ToRadians(_currentPosition.Latitude));
                
                _currentPosition.Latitude += deltaLat;
                _currentPosition.Longitude += deltaLon;
                
                // Add small random variations for realistic GPS noise
                _currentPosition.Latitude += (_random.NextDouble() - 0.5) * 0.0001;
                _currentPosition.Longitude += (_random.NextDouble() - 0.5) * 0.0001;
            }
            else
            {
                _currentPosition.Speed = 0;
            }
            
            // Update other GPS parameters
            _currentPosition.Satellites = 8 + _random.Next(-2, 3);
            _currentPosition.Hdop = 1.0 + (_random.NextDouble() - 0.5) * 0.5;
            _currentPosition.Altitude = 50 + (_random.NextDouble() - 0.5) * 20;
        }
        
        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return 6371 * c; // Earth's radius in km
        }
        
        private static double CalculateBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = ToRadians(lon2 - lon1);
            var y = Math.Sin(dLon) * Math.Cos(ToRadians(lat2));
            var x = Math.Cos(ToRadians(lat1)) * Math.Sin(ToRadians(lat2)) -
                    Math.Sin(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * Math.Cos(dLon);
            var bearing = ToDegrees(Math.Atan2(y, x));
            return (bearing + 360) % 360;
        }
        
        private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
        private static double ToDegrees(double radians) => radians * 180.0 / Math.PI;
        
        public void Dispose()
        {
            _updateTimer?.Dispose();
        }
    }
}
