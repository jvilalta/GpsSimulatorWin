# GPS Simulator

A C# Windows Forms application that simulates GPS data transmission with a graphical user interface for GPS simulation and visualization. Features network connectivity to broadcast NMEA data over TCP and UDP protocols.

## Features

- **Real-time GPS Simulation**: Generates realistic GPS coordinates, speed, heading, and other telemetry data
- **Interactive Controls**: Start/stop simulation, set position, configure speed, and manage waypoints
- **Degrees/Minutes Input**: User-friendly coordinate entry using degrees and decimal minutes format (DD° MM.MMMM')
- **Tab Navigation**: Seamless keyboard navigation between coordinate input fields
- **Waypoint Navigation**: Add multiple waypoints for the simulator to navigate through automatically
- **NMEA Output**: Displays standard NMEA GPS sentences in real-time
- **Network Broadcasting**: Transmit NMEA data over TCP server and/or UDP broadcast
- **Port Configuration**: Configurable TCP and UDP ports for network transmission
- **Client Management**: Monitor connected TCP clients and network activity
- **Live Telemetry Display**: Shows current latitude, longitude, speed, heading, altitude, and satellite count

## Network Features

### TCP Server
- Configurable port (default: 2947, standard GPS daemon port)
- Multiple client support
- Real-time client connection monitoring
- Automatic client disconnect handling

### UDP Broadcast
- Configurable port (default: 2948)
- Broadcast NMEA sentences to network
- No client connection required

### Supported Protocols
- **NMEA 0183**: Standard GPS data format
- **TCP**: Reliable connection-based transmission
- **UDP**: Lightweight broadcast transmission

## Getting Started

### Prerequisites

- .NET 8.0 or later
- Windows operating system (for Windows Forms)

### Building and Running

1. Clone or download the project
2. Open a terminal in the project directory
3. Build the project:
   ```
   dotnet build
   ```
4. Run the application:
   ```
   dotnet run
   ```

### Using the Simulator

#### GPS Simulation
1. **Starting Simulation**: Click the "Start Simulation" button to begin GPS movement
2. **Setting Position**: Enter coordinates using degrees and decimal minutes format:
   - **Degrees**: Enter whole degrees (0-90 for latitude, 0-180 for longitude)
   - **Minutes**: Enter decimal minutes (0.0000-59.9999)
   - **Direction**: Select N/S for latitude, E/W for longitude
   - Example: San Francisco = 37° 46.494' N, 122° 25.164' W
3. **Adding Waypoints**: Enter coordinates in the same format and click "Add Waypoint" to create a navigation route
4. **Adjusting Speed**: Modify the speed value to change how fast the simulated GPS moves
5. **NMEA Output**: View the generated NMEA GPS sentences in the output text area
6. **Tab Navigation**: Use Tab key to move between coordinate input fields for easy data entry

#### Network Configuration
1. **Enable TCP Server**: Check the "Enable TCP Server" box and set the desired port (default: 2947)
2. **Enable UDP Broadcast**: Check the "Enable UDP Broadcast" box and set the desired port (default: 2948)
3. **Start Network**: Click "Start Network" to begin broadcasting NMEA data
4. **Monitor Activity**: View client connections and network events in the Network Log

#### Connecting GPS Applications
Once the network service is running, GPS applications can connect to receive NMEA data:

**TCP Connection**: 
- Host: `localhost` (or your computer's IP address)
- Port: `2947` (or your configured port)
- Protocol: TCP

**UDP Reception**:
- Port: `2948` (or your configured port)
- Protocol: UDP Broadcast

Popular GPS applications that can connect:
- **gpsd clients** (Linux/Unix GPS daemon clients)
- **OpenCPN** (Marine navigation software)
- **QGIS** with GPS plugins
- Custom applications using GPS libraries

## Project Structure

- `Form1.cs` - Main Windows Forms UI and event handling
- `Form1.Designer.cs` - UI component initialization and layout
- `GpsSimulatorEngine.cs` - Core GPS simulation logic and movement algorithms
- `GpsData.cs` - GPS data model and NMEA sentence generation
- `NmeaNetworkService.cs` - TCP/UDP network service for NMEA data transmission

## Technical Details

The simulator uses realistic GPS algorithms including:
- Great circle distance calculations for accurate positioning
- Bearing calculations for proper heading navigation
- Speed smoothing and turning radius simulation
- GPS noise and variation modeling
- Standard NMEA sentence formatting (GPRMC)

### Network Implementation
- **Asynchronous TCP server** with multi-client support
- **UDP broadcast** for connectionless transmission
- **Thread-safe** network operations
- **Error handling** and client disconnect detection
- **Real-time logging** of network events

## Origins

This project is a C# Windows Forms port of a Tcl/Tk GPS simulator, providing the same functionality with a modern .NET interface.

## License

This project is open source and available under standard licensing terms.
