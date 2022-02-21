namespace PW.MacroDeck.VoicemeeterPlugin.Views
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
            this.deviceSelectorBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // deviceSelectorBox
            // 
            this.deviceSelectorBox.FormattingEnabled = true;
            this.deviceSelectorBox.Location = new System.Drawing.Point(121, 79);
            this.deviceSelectorBox.Name = "deviceSelectorBox";
            this.deviceSelectorBox.Size = new System.Drawing.Size(543, 31);
            this.deviceSelectorBox.TabIndex = 0;
            // 
            // DeviceSelectorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deviceSelectorBox);
            this.Name = "DeviceSelectorView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox deviceSelectorBox;
    }
}