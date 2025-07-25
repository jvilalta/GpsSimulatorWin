using System;

namespace GpsSimulator
{
    /// <summary>
    /// Represents GPS coordinate data with latitude, longitude, and associated metadata
    /// </summary>
    public class GpsData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; } // in knots
        public double Heading { get; set; } // in degrees
        public double Altitude { get; set; } // in meters
        public DateTime Timestamp { get; set; }
        public int Satellites { get; set; }
        public double Hdop { get; set; } // Horizontal dilution of precision
        public char FixQuality { get; set; } = 'A'; // A = Active, V = Void
        
        public GpsData()
        {
            Timestamp = DateTime.UtcNow;
            Satellites = 8;
            Hdop = 1.0;
        }
        
        /// <summary>
        /// Generates NMEA-style GPS sentence (simplified GPRMC)
        /// </summary>
        public string ToNmeaString()
        {
            var time = Timestamp.ToString("HHmmss");
            var date = Timestamp.ToString("ddMMyy");
            
            var latDeg = (int)Math.Abs(Latitude);
            var latMin = (Math.Abs(Latitude) - latDeg) * 60;
            var latDir = Latitude >= 0 ? "N" : "S";
            var latStr = $"{latDeg:00}{latMin:00.000}";
            
            var lonDeg = (int)Math.Abs(Longitude);
            var lonMin = (Math.Abs(Longitude) - lonDeg) * 60;
            var lonDir = Longitude >= 0 ? "E" : "W";
            var lonStr = $"{lonDeg:000}{lonMin:00.000}";
            
            var speedKnots = Speed.ToString("F1");
            var course = Heading.ToString("F1");
            
            var sentence = $"$GPRMC,{time},{FixQuality},{latStr},{latDir},{lonStr},{lonDir},{speedKnots},{course},{date},,";
            
            // Calculate checksum
            byte checksum = 0;
            for (int i = 1; i < sentence.Length; i++)
            {
                checksum ^= (byte)sentence[i];
            }
            
            return $"{sentence}*{checksum:X2}";
        }
    }
}
