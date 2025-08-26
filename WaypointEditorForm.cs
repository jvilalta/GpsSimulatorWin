using System;
using System.Drawing;
using System.Windows.Forms;

namespace GpsSimulator
{
    /// <summary>
    /// Modal dialog for editing waypoint coordinates
    /// </summary>
    public partial class WaypointEditorForm : Form
    {
        private TextBox _latitudeDegreesTextBox;
        private TextBox _latitudeMinutesTextBox;
        private TextBox _longitudeDegreesTextBox;
        private TextBox _longitudeMinutesTextBox;
        private ComboBox _latitudeDirectionComboBox;
        private ComboBox _longitudeDirectionComboBox;
        private Button _okButton;
        private Button _cancelButton;
        private Button _deleteButton;
        
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public bool DeleteRequested { get; private set; }
        
        public WaypointEditorForm(double latitude, double longitude, int waypointIndex)
        {
            InitializeComponent();
            
            Text = $"Edit Waypoint {waypointIndex + 1}";
            
            // Set initial values
            var (latDeg, latMin) = DecimalDegreesToDegreesMinutes(latitude);
            var (lonDeg, lonMin) = DecimalDegreesToDegreesMinutes(longitude);
            
            _latitudeDegreesTextBox.Text = latDeg.ToString();
            _latitudeMinutesTextBox.Text = latMin.ToString("F4");
            _latitudeDirectionComboBox.SelectedItem = latitude >= 0 ? "N" : "S";
            
            _longitudeDegreesTextBox.Text = lonDeg.ToString();
            _longitudeMinutesTextBox.Text = lonMin.ToString("F4");
            _longitudeDirectionComboBox.SelectedItem = longitude >= 0 ? "E" : "W";
        }
        
        private void InitializeComponent()
        {
            _latitudeDegreesTextBox = new TextBox();
            _latitudeMinutesTextBox = new TextBox();
            _longitudeDegreesTextBox = new TextBox();
            _longitudeMinutesTextBox = new TextBox();
            _latitudeDirectionComboBox = new ComboBox();
            _longitudeDirectionComboBox = new ComboBox();
            _okButton = new Button();
            _cancelButton = new Button();
            _deleteButton = new Button();
            
            var latLabel = new Label();
            var lonLabel = new Label();
            var latDegreeSymbol = new Label();
            var latMinuteSymbol = new Label();
            var lonDegreeSymbol = new Label();
            var lonMinuteSymbol = new Label();
            
            SuspendLayout();
            
            // Form properties
            ClientSize = new Size(400, 180);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = _okButton;
            CancelButton = _cancelButton;
            
            // Latitude label
            latLabel.Location = new Point(20, 30);
            latLabel.Size = new Size(60, 23);
            latLabel.Text = "Latitude:";
            
            // Latitude degrees
            _latitudeDegreesTextBox.Location = new Point(90, 30);
            _latitudeDegreesTextBox.Size = new Size(40, 23);
            _latitudeDegreesTextBox.TabIndex = 0;
            
            // Latitude degree symbol
            latDegreeSymbol.Location = new Point(135, 30);
            latDegreeSymbol.Size = new Size(15, 23);
            latDegreeSymbol.Text = "°";
            
            // Latitude minutes
            _latitudeMinutesTextBox.Location = new Point(155, 30);
            _latitudeMinutesTextBox.Size = new Size(60, 23);
            _latitudeMinutesTextBox.TabIndex = 1;
            
            // Latitude minute symbol
            latMinuteSymbol.Location = new Point(220, 30);
            latMinuteSymbol.Size = new Size(15, 23);
            latMinuteSymbol.Text = "'";
            
            // Latitude direction
            _latitudeDirectionComboBox.Location = new Point(240, 30);
            _latitudeDirectionComboBox.Size = new Size(40, 23);
            _latitudeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _latitudeDirectionComboBox.Items.AddRange(new object[] { "N", "S" });
            _latitudeDirectionComboBox.TabIndex = 2;
            
            // Longitude label
            lonLabel.Location = new Point(20, 70);
            lonLabel.Size = new Size(60, 23);
            lonLabel.Text = "Longitude:";
            
            // Longitude degrees
            _longitudeDegreesTextBox.Location = new Point(90, 70);
            _longitudeDegreesTextBox.Size = new Size(40, 23);
            _longitudeDegreesTextBox.TabIndex = 3;
            
            // Longitude degree symbol
            lonDegreeSymbol.Location = new Point(135, 70);
            lonDegreeSymbol.Size = new Size(15, 23);
            lonDegreeSymbol.Text = "°";
            
            // Longitude minutes
            _longitudeMinutesTextBox.Location = new Point(155, 70);
            _longitudeMinutesTextBox.Size = new Size(60, 23);
            _longitudeMinutesTextBox.TabIndex = 4;
            
            // Longitude minute symbol
            lonMinuteSymbol.Location = new Point(220, 70);
            lonMinuteSymbol.Size = new Size(15, 23);
            lonMinuteSymbol.Text = "'";
            
            // Longitude direction
            _longitudeDirectionComboBox.Location = new Point(240, 70);
            _longitudeDirectionComboBox.Size = new Size(40, 23);
            _longitudeDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _longitudeDirectionComboBox.Items.AddRange(new object[] { "E", "W" });
            _longitudeDirectionComboBox.TabIndex = 5;
            
            // OK button
            _okButton.Location = new Point(140, 120);
            _okButton.Size = new Size(75, 30);
            _okButton.Text = "OK";
            _okButton.TabIndex = 6;
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Click += OkButton_Click;
            
            // Cancel button
            _cancelButton.Location = new Point(225, 120);
            _cancelButton.Size = new Size(75, 30);
            _cancelButton.Text = "Cancel";
            _cancelButton.TabIndex = 7;
            _cancelButton.UseVisualStyleBackColor = true;
            _cancelButton.Click += CancelButton_Click;
            
            // Delete button
            _deleteButton.Location = new Point(20, 120);
            _deleteButton.Size = new Size(75, 30);
            _deleteButton.Text = "Delete";
            _deleteButton.TabIndex = 8;
            _deleteButton.UseVisualStyleBackColor = true;
            _deleteButton.BackColor = Color.LightCoral;
            _deleteButton.Click += DeleteButton_Click;
            
            // Add controls to form
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
            Controls.Add(_okButton);
            Controls.Add(_cancelButton);
            Controls.Add(_deleteButton);
            
            ResumeLayout(false);
        }
        
        private void OkButton_Click(object? sender, EventArgs e)
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
                
                Latitude = lat;
                Longitude = lon;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Please enter valid latitude and longitude values in degrees and decimal minutes.", "Invalid Input", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void CancelButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this waypoint?", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                DeleteRequested = true;
                DialogResult = DialogResult.OK;
                Close();
            }
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
    }
}