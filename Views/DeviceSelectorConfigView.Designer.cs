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
            // DeviceSelectorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.deviceSelectorBox);
            this.Controls.Add(this.labelAction);
            this.Controls.Add(this.actionSelectorBox);
            this.Name = "DeviceSelectorView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDevice;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox deviceSelectorBox;
        private System.Windows.Forms.Label labelAction;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox actionSelectorBox;
    }
}