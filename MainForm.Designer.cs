namespace GpsSimulator;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _statusLabel = new Label();
        _latitudeLabel = new Label();
        _longitudeLabel = new Label();
        _speedLabel = new Label();
        _headingLabel = new Label();
        _altitudeLabel = new Label();
        _satellitesLabel = new Label();
        _nmeaLabel = new Label();
        _networkStatusLabel = new Label();
        _latitudeTextBox = new TextBox();
        _longitudeTextBox = new TextBox();
        _speedTextBox = new TextBox();
        _nmeaOutput = new TextBox();
        _tcpPortTextBox = new TextBox();
        _udpPortTextBox = new TextBox();
        _latitudeDegreesTextBox = new TextBox();
        _latitudeMinutesTextBox = new TextBox();
        _longitudeDegreesTextBox = new TextBox();
        _longitudeMinutesTextBox = new TextBox();
        _latitudeDirectionComboBox = new ComboBox();
        _longitudeDirectionComboBox = new ComboBox();
        _startButton = new Button();
        _stopButton = new Button();
        _setPositionButton = new Button();
        _addWaypointButton = new Button();
        _clearWaypointsButton = new Button();
        _startNetworkButton = new Button();
        _stopNetworkButton = new Button();
        _enableTcpCheckBox = new CheckBox();
        _enableUdpCheckBox = new CheckBox();
        _waypointsListBox = new ListBox();
        _networkLogListBox = new ListBox();
        coordinateInstructionLabel = new Label();
        latLabel = new Label();
        latDegreeSymbol = new Label();
        latMinuteSymbol = new Label();
        lonLabel = new Label();
        lonDegreeSymbol = new Label();
        lonMinuteSymbol = new Label();
        speedLabel = new Label();
        networkLabel = new Label();
        tcpPortLabel = new Label();
        udpPortLabel = new Label();
        waypointsLabel = new Label();
        networkLogLabel = new Label();
        SuspendLayout();
        // 
        // _statusLabel
        // 
        _statusLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _statusLabel.ForeColor = Color.Red;
        _statusLabel.Location = new Point(20, 20);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(100, 23);
        _statusLabel.TabIndex = 0;
        _statusLabel.Text = "Stopped";
        // 
        // _latitudeLabel
        // 
        _latitudeLabel.Location = new Point(20, 60);
        _latitudeLabel.Name = "_latitudeLabel";
        _latitudeLabel.Size = new Size(200, 23);
        _latitudeLabel.TabIndex = 2;
        _latitudeLabel.Text = "Latitude: 0.000000°";
        // 
        // _longitudeLabel
        // 
        _longitudeLabel.Location = new Point(20, 90);
        _longitudeLabel.Name = "_longitudeLabel";
        _longitudeLabel.Size = new Size(200, 23);
        _longitudeLabel.TabIndex = 3;
        _longitudeLabel.Text = "Longitude: 0.000000°";
        // 
        // _speedLabel
        // 
        _speedLabel.Location = new Point(20, 120);
        _speedLabel.Name = "_speedLabel";
        _speedLabel.Size = new Size(200, 23);
        _speedLabel.TabIndex = 4;
        _speedLabel.Text = "Speed: 0.0 knots";
        // 
        // _headingLabel
        // 
        _headingLabel.Location = new Point(20, 150);
        _headingLabel.Name = "_headingLabel";
        _headingLabel.Size = new Size(200, 23);
        _headingLabel.TabIndex = 5;
        _headingLabel.Text = "Heading: 0.0°";
        // 
        // _altitudeLabel
        // 
        _altitudeLabel.Location = new Point(20, 180);
        _altitudeLabel.Name = "_altitudeLabel";
        _altitudeLabel.Size = new Size(200, 23);
        _altitudeLabel.TabIndex = 6;
        _altitudeLabel.Text = "Altitude: 0 m";
        // 
        // _satellitesLabel
        // 
        _satellitesLabel.Location = new Point(20, 210);
        _satellitesLabel.Name = "_satellitesLabel";
        _satellitesLabel.Size = new Size(200, 23);
        _satellitesLabel.TabIndex = 7;
        _satellitesLabel.Text = "Satellites: 0";
        // 
        // _nmeaLabel
        // 
        _nmeaLabel.Location = new Point(20, 450);
        _nmeaLabel.Name = "_nmeaLabel";
        _nmeaLabel.Size = new Size(100, 23);
        _nmeaLabel.TabIndex = 8;
        _nmeaLabel.Text = "NMEA Output:";
        // 
        // _networkStatusLabel
        // 
        _networkStatusLabel.Font = new Font("Segoe UI", 10F);
        _networkStatusLabel.ForeColor = Color.Gray;
        _networkStatusLabel.Location = new Point(140, 20);
        _networkStatusLabel.Name = "_networkStatusLabel";
        _networkStatusLabel.Size = new Size(300, 23);
        _networkStatusLabel.TabIndex = 1;
        _networkStatusLabel.Text = "Network: Offline";
        // 
        // _latitudeTextBox
        // 
        _latitudeTextBox.Location = new Point(-1000, -1000);
        _latitudeTextBox.Name = "_latitudeTextBox";
        _latitudeTextBox.Size = new Size(1, 23);
        _latitudeTextBox.TabIndex = 17;
        _latitudeTextBox.Visible = false;
        // 
        // _longitudeTextBox
        // 
        _longitudeTextBox.Location = new Point(-1000, -1000);
        _longitudeTextBox.Name = "_longitudeTextBox";
        _longitudeTextBox.Size = new Size(1, 23);
        _longitudeTextBox.TabIndex = 18;
        _longitudeTextBox.Visible = false;
        // 
        // _speedTextBox
        // 
        _speedTextBox.Location = new Point(370, 120);
        _speedTextBox.Name = "_speedTextBox";
        _speedTextBox.Size = new Size(60, 23);
        _speedTextBox.TabIndex = 7;
        _speedTextBox.Text = "30";
        _speedTextBox.TextChanged += SpeedTextBox_TextChanged;
        // 
        // _nmeaOutput
        // 
        _nmeaOutput.Font = new Font("Consolas", 9F);
        _nmeaOutput.Location = new Point(20, 480);
        _nmeaOutput.Multiline = true;
        _nmeaOutput.Name = "_nmeaOutput";
        _nmeaOutput.ReadOnly = true;
        _nmeaOutput.ScrollBars = ScrollBars.Vertical;
        _nmeaOutput.Size = new Size(950, 150);
        _nmeaOutput.TabIndex = 32;
        // 
        // _tcpPortTextBox
        // 
        _tcpPortTextBox.Location = new Point(750, 87);
        _tcpPortTextBox.Name = "_tcpPortTextBox";
        _tcpPortTextBox.Size = new Size(80, 23);
        _tcpPortTextBox.TabIndex = 22;
        _tcpPortTextBox.Text = "2947";
        // 
        // _udpPortTextBox
        // 
        _udpPortTextBox.Location = new Point(750, 117);
        _udpPortTextBox.Name = "_udpPortTextBox";
        _udpPortTextBox.Size = new Size(80, 23);
        _udpPortTextBox.TabIndex = 25;
        _udpPortTextBox.Text = "2948";
        // 
        // _latitudeDegreesTextBox
        // 
        _latitudeDegreesTextBox.Location = new Point(350, 60);
        _latitudeDegreesTextBox.Name = "_latitudeDegreesTextBox";
        _latitudeDegreesTextBox.Size = new Size(40, 23);
        _latitudeDegreesTextBox.TabIndex = 1;
        _latitudeDegreesTextBox.Text = "37";
        // 
        // _latitudeMinutesTextBox
        // 
        _latitudeMinutesTextBox.Location = new Point(415, 60);
        _latitudeMinutesTextBox.Name = "_latitudeMinutesTextBox";
        _latitudeMinutesTextBox.Size = new Size(60, 23);
        _latitudeMinutesTextBox.TabIndex = 2;
        _latitudeMinutesTextBox.Text = "46.494";
        // 
        // _longitudeDegreesTextBox
        // 
        _longitudeDegreesTextBox.Location = new Point(350, 90);
        _longitudeDegreesTextBox.Name = "_longitudeDegreesTextBox";
        _longitudeDegreesTextBox.Size = new Size(40, 23);
        _longitudeDegreesTextBox.TabIndex = 4;
        _longitudeDegreesTextBox.Text = "122";
        // 
        // _longitudeMinutesTextBox
        // 
        _longitudeMinutesTextBox.Location = new Point(415, 90);
        _longitudeMinutesTextBox.Name = "_longitudeMinutesTextBox";
        _longitudeMinutesTextBox.Size = new Size(60, 23);
        _longitudeMinutesTextBox.TabIndex = 5;
        _longitudeMinutesTextBox.Text = "25.164";
        // 
        // _latitudeDirectionComboBox
        // 
        _latitudeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _latitudeDirectionComboBox.Items.AddRange(new object[] { "N", "S" });
        _latitudeDirectionComboBox.Location = new Point(500, 60);
        _latitudeDirectionComboBox.Name = "_latitudeDirectionComboBox";
        _latitudeDirectionComboBox.Size = new Size(40, 23);
        _latitudeDirectionComboBox.TabIndex = 3;
        // 
        // _longitudeDirectionComboBox
        // 
        _longitudeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        _longitudeDirectionComboBox.Items.AddRange(new object[] { "E", "W" });
        _longitudeDirectionComboBox.Location = new Point(500, 90);
        _longitudeDirectionComboBox.Name = "_longitudeDirectionComboBox";
        _longitudeDirectionComboBox.Size = new Size(40, 23);
        _longitudeDirectionComboBox.TabIndex = 6;
        // 
        // _startButton
        // 
        _startButton.Location = new Point(280, 160);
        _startButton.Name = "_startButton";
        _startButton.Size = new Size(100, 30);
        _startButton.TabIndex = 8;
        _startButton.Text = "Start Simulation";
        _startButton.Click += StartButton_Click;
        // 
        // _stopButton
        // 
        _stopButton.Enabled = false;
        _stopButton.Location = new Point(390, 160);
        _stopButton.Name = "_stopButton";
        _stopButton.Size = new Size(100, 30);
        _stopButton.TabIndex = 9;
        _stopButton.Text = "Stop Simulation";
        _stopButton.Click += StopButton_Click;
        // 
        // _setPositionButton
        // 
        _setPositionButton.Location = new Point(280, 200);
        _setPositionButton.Name = "_setPositionButton";
        _setPositionButton.Size = new Size(100, 30);
        _setPositionButton.TabIndex = 10;
        _setPositionButton.Text = "Set Position";
        _setPositionButton.Click += SetPositionButton_Click;
        // 
        // _addWaypointButton
        // 
        _addWaypointButton.Location = new Point(390, 200);
        _addWaypointButton.Name = "_addWaypointButton";
        _addWaypointButton.Size = new Size(100, 30);
        _addWaypointButton.TabIndex = 11;
        _addWaypointButton.Text = "Add Waypoint";
        _addWaypointButton.Click += AddWaypointButton_Click;
        // 
        // _clearWaypointsButton
        // 
        _clearWaypointsButton.Location = new Point(280, 240);
        _clearWaypointsButton.Name = "_clearWaypointsButton";
        _clearWaypointsButton.Size = new Size(120, 30);
        _clearWaypointsButton.TabIndex = 12;
        _clearWaypointsButton.Text = "Clear Waypoints";
        _clearWaypointsButton.Click += ClearWaypointsButton_Click;
        // 
        // _startNetworkButton
        // 
        _startNetworkButton.Location = new Point(520, 160);
        _startNetworkButton.Name = "_startNetworkButton";
        _startNetworkButton.Size = new Size(100, 30);
        _startNetworkButton.TabIndex = 26;
        _startNetworkButton.Text = "Start Network";
        _startNetworkButton.Click += StartNetworkButton_Click;
        // 
        // _stopNetworkButton
        // 
        _stopNetworkButton.Enabled = false;
        _stopNetworkButton.Location = new Point(630, 160);
        _stopNetworkButton.Name = "_stopNetworkButton";
        _stopNetworkButton.Size = new Size(100, 30);
        _stopNetworkButton.TabIndex = 27;
        _stopNetworkButton.Text = "Stop Network";
        _stopNetworkButton.Click += StopNetworkButton_Click;
        // 
        // _enableTcpCheckBox
        // 
        _enableTcpCheckBox.Checked = true;
        _enableTcpCheckBox.CheckState = CheckState.Checked;
        _enableTcpCheckBox.Location = new Point(548, 87);
        _enableTcpCheckBox.Name = "_enableTcpCheckBox";
        _enableTcpCheckBox.Size = new Size(130, 23);
        _enableTcpCheckBox.TabIndex = 20;
        _enableTcpCheckBox.Text = "Enable TCP Server";
        // 
        // _enableUdpCheckBox
        // 
        _enableUdpCheckBox.Location = new Point(548, 116);
        _enableUdpCheckBox.Name = "_enableUdpCheckBox";
        _enableUdpCheckBox.Size = new Size(130, 23);
        _enableUdpCheckBox.TabIndex = 23;
        _enableUdpCheckBox.Text = "Enable UDP Broadcast";
        // 
        // _waypointsListBox
        // 
        _waypointsListBox.ItemHeight = 15;
        _waypointsListBox.Location = new Point(20, 310);
        _waypointsListBox.Name = "_waypointsListBox";
        _waypointsListBox.Size = new Size(200, 109);
        _waypointsListBox.TabIndex = 29;
        // 
        // _networkLogListBox
        // 
        _networkLogListBox.ItemHeight = 15;
        _networkLogListBox.Location = new Point(240, 310);
        _networkLogListBox.Name = "_networkLogListBox";
        _networkLogListBox.Size = new Size(300, 109);
        _networkLogListBox.TabIndex = 31;
        // 
        // coordinateInstructionLabel
        // 
        coordinateInstructionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
        coordinateInstructionLabel.ForeColor = Color.Gray;
        coordinateInstructionLabel.Location = new Point(280, 40);
        coordinateInstructionLabel.Name = "coordinateInstructionLabel";
        coordinateInstructionLabel.Size = new Size(350, 20);
        coordinateInstructionLabel.TabIndex = 9;
        coordinateInstructionLabel.Text = "Enter coordinates in degrees and decimal minutes (e.g., 37° 46.494' N)";
        // 
        // latLabel
        // 
        latLabel.Location = new Point(280, 60);
        latLabel.Name = "latLabel";
        latLabel.Size = new Size(60, 23);
        latLabel.TabIndex = 10;
        latLabel.Text = "Latitude:";
        // 
        // latDegreeSymbol
        // 
        latDegreeSymbol.Location = new Point(395, 60);
        latDegreeSymbol.Name = "latDegreeSymbol";
        latDegreeSymbol.Size = new Size(15, 23);
        latDegreeSymbol.TabIndex = 11;
        latDegreeSymbol.Text = "°";
        // 
        // latMinuteSymbol
        // 
        latMinuteSymbol.Location = new Point(480, 60);
        latMinuteSymbol.Name = "latMinuteSymbol";
        latMinuteSymbol.Size = new Size(15, 23);
        latMinuteSymbol.TabIndex = 12;
        latMinuteSymbol.Text = "'";
        // 
        // lonLabel
        // 
        lonLabel.Location = new Point(280, 90);
        lonLabel.Name = "lonLabel";
        lonLabel.Size = new Size(60, 23);
        lonLabel.TabIndex = 13;
        lonLabel.Text = "Longitude:";
        // 
        // lonDegreeSymbol
        // 
        lonDegreeSymbol.Location = new Point(395, 90);
        lonDegreeSymbol.Name = "lonDegreeSymbol";
        lonDegreeSymbol.Size = new Size(15, 23);
        lonDegreeSymbol.TabIndex = 14;
        lonDegreeSymbol.Text = "°";
        // 
        // lonMinuteSymbol
        // 
        lonMinuteSymbol.Location = new Point(480, 90);
        lonMinuteSymbol.Name = "lonMinuteSymbol";
        lonMinuteSymbol.Size = new Size(15, 23);
        lonMinuteSymbol.TabIndex = 15;
        lonMinuteSymbol.Text = "'";
        // 
        // speedLabel
        // 
        speedLabel.Location = new Point(280, 120);
        speedLabel.Name = "speedLabel";
        speedLabel.Size = new Size(80, 23);
        speedLabel.TabIndex = 16;
        speedLabel.Text = "Speed (knots):";
        // 
        // networkLabel
        // 
        networkLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        networkLabel.Location = new Point(546, 61);
        networkLabel.Name = "networkLabel";
        networkLabel.Size = new Size(150, 23);
        networkLabel.TabIndex = 19;
        networkLabel.Text = "Network Configuration";
        // 
        // tcpPortLabel
        // 
        tcpPortLabel.Location = new Point(684, 90);
        tcpPortLabel.Name = "tcpPortLabel";
        tcpPortLabel.Size = new Size(60, 23);
        tcpPortLabel.TabIndex = 21;
        tcpPortLabel.Text = "TCP Port:";
        // 
        // udpPortLabel
        // 
        udpPortLabel.Location = new Point(684, 120);
        udpPortLabel.Name = "udpPortLabel";
        udpPortLabel.Size = new Size(60, 23);
        udpPortLabel.TabIndex = 24;
        udpPortLabel.Text = "UDP Port:";
        // 
        // waypointsLabel
        // 
        waypointsLabel.Location = new Point(20, 280);
        waypointsLabel.Name = "waypointsLabel";
        waypointsLabel.Size = new Size(80, 23);
        waypointsLabel.TabIndex = 28;
        waypointsLabel.Text = "Waypoints:";
        // 
        // networkLogLabel
        // 
        networkLogLabel.Location = new Point(240, 280);
        networkLogLabel.Name = "networkLogLabel";
        networkLogLabel.Size = new Size(100, 23);
        networkLogLabel.TabIndex = 30;
        networkLogLabel.Text = "Network Log:";
        // 
        // MainForm
        // 
        ClientSize = new Size(984, 661);
        Controls.Add(_statusLabel);
        Controls.Add(_networkStatusLabel);
        Controls.Add(_latitudeLabel);
        Controls.Add(_longitudeLabel);
        Controls.Add(_speedLabel);
        Controls.Add(_headingLabel);
        Controls.Add(_altitudeLabel);
        Controls.Add(_satellitesLabel);
        Controls.Add(_nmeaLabel);
        Controls.Add(coordinateInstructionLabel);
        Controls.Add(latLabel);
        Controls.Add(_latitudeDegreesTextBox);
        Controls.Add(latDegreeSymbol);
        Controls.Add(_latitudeMinutesTextBox);
        Controls.Add(latMinuteSymbol);
        Controls.Add(_latitudeDirectionComboBox);
        Controls.Add(lonLabel);
        Controls.Add(_longitudeDegreesTextBox);
        Controls.Add(lonDegreeSymbol);
        Controls.Add(_longitudeMinutesTextBox);
        Controls.Add(lonMinuteSymbol);
        Controls.Add(_longitudeDirectionComboBox);
        Controls.Add(speedLabel);
        Controls.Add(_speedTextBox);
        Controls.Add(_latitudeTextBox);
        Controls.Add(_longitudeTextBox);
        Controls.Add(_startButton);
        Controls.Add(_stopButton);
        Controls.Add(_setPositionButton);
        Controls.Add(_addWaypointButton);
        Controls.Add(_clearWaypointsButton);
        Controls.Add(networkLabel);
        Controls.Add(_enableTcpCheckBox);
        Controls.Add(tcpPortLabel);
        Controls.Add(_tcpPortTextBox);
        Controls.Add(_enableUdpCheckBox);
        Controls.Add(udpPortLabel);
        Controls.Add(_udpPortTextBox);
        Controls.Add(_startNetworkButton);
        Controls.Add(_stopNetworkButton);
        Controls.Add(waypointsLabel);
        Controls.Add(_waypointsListBox);
        Controls.Add(networkLogLabel);
        Controls.Add(_networkLogListBox);
        Controls.Add(_nmeaOutput);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GPS Simulator";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label coordinateInstructionLabel;
    private Label latLabel;
    private Label latDegreeSymbol;
    private Label latMinuteSymbol;
    private Label lonLabel;
    private Label lonDegreeSymbol;
    private Label lonMinuteSymbol;
    private Label speedLabel;
    private Label networkLabel;
    private Label tcpPortLabel;
    private Label udpPortLabel;
    private Label waypointsLabel;
    private Label networkLogLabel;
}
