namespace PW.VoicemeeterPlugin.Views
{
    partial class DeviceSelectorConfigView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelDevice = new System.Windows.Forms.Label();
            this.deviceSelectorBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.actionSelectorBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.labelSlider = new System.Windows.Forms.Label();
            this.actionSliderValue = new System.Windows.Forms.NumericUpDown();
            this.SuspendLayout();
            // 
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(40, 45);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(84, 23);
            this.labelDevice.TabIndex = 0;
            this.labelDevice.Text = "Devices";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deviceSelectorBox
            // 
            this.deviceSelectorBox.Location = new System.Drawing.Point(60, 79);
            this.deviceSelectorBox.Name = "deviceSelectorBox";
            this.deviceSelectorBox.Size = new System.Drawing.Size(543, 31);
            this.deviceSelectorBox.TabIndex = 0;
            this.deviceSelectorBox.SelectedIndexChanged += DeviceSelectorBox_SelectedIndexChanged;
            // 
            // labelDevice
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(40, 120);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(84, 23);
            this.labelAction.TabIndex = 0;
            this.labelAction.Text = "Action";
            this.labelAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionSelectorBox
            // 
            this.actionSelectorBox.Location = new System.Drawing.Point(60, 154);
            this.actionSelectorBox.Name = "actionSelectorBox";
            this.actionSelectorBox.Size = new System.Drawing.Size(543, 31);
            this.actionSelectorBox.TabIndex = 0;
            this.actionSelectorBox.SelectedIndexChanged += ActionSelectorBox_SelectedIndexChanged;
            // 
            // labelSlider
            // 
            this.labelSlider.AutoSize = true;
            this.labelSlider.Location = new System.Drawing.Point(40, 195);
            this.labelSlider.Name = "labelSlider";
            this.labelSlider.Size = new System.Drawing.Size(84, 23);
            this.labelSlider.TabIndex = 0;
            this.labelSlider.Text = "Value";
            this.labelSlider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionSliderValue
            // 
            this.actionSliderValue.Location = new System.Drawing.Point(60, 229);
            this.actionSliderValue.Name = "actionSliderValue";
            this.actionSliderValue.DecimalPlaces = 1;
            this.actionSliderValue.Increment = (decimal)0.5;
            this.actionSliderValue.Minimum = -10;
            this.actionSliderValue.Maximum = 10;
            this.actionSliderValue.Size = new System.Drawing.Size(75, 31);
            this.actionSliderValue.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.actionSliderValue.TabIndex = 0;
            // 
            // DeviceSelectorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.deviceSelectorBox);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.actionSelectorBox);
            this.Controls.Add(this.labelSlider);
            this.Controls.Add(this.actionSliderValue);
            this.Name = "DeviceSelectorView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDevice;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox deviceSelectorBox;
        private System.Windows.Forms.Label labelAction;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox actionSelectorBox;
        private System.Windows.Forms.Label labelSlider;
        private System.Windows.Forms.NumericUpDown actionSliderValue;
    }
}