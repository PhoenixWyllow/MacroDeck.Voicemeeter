namespace PW.VoicemeeterPlugin.Views
{
    partial class CommandActionConfigView
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
            this.labelCommand = new System.Windows.Forms.Label();
            this.commandSelectorBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.labelCommandValue = new System.Windows.Forms.Label();
            this.commandValueBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.SuspendLayout();
            // 
            // labelCommand
            // 
            this.labelCommand.AutoSize = true;
            this.labelCommand.Location = new System.Drawing.Point(40, 45);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Size = new System.Drawing.Size(84, 23);
            this.labelCommand.TabIndex = 0;
            this.labelCommand.Text = "CommandType";
            this.labelCommand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commandSelectorBox
            // 
            this.commandSelectorBox.Location = new System.Drawing.Point(60, 79);
            this.commandSelectorBox.Name = "commandSelectorBox";
            this.commandSelectorBox.Size = new System.Drawing.Size(543, 31);
            this.commandSelectorBox.TabIndex = 0;
            this.commandSelectorBox.SelectedIndexChanged += DeviceSelectorBox_SelectedIndexChanged;
            // 
            // labelCommand
            // 
            this.labelCommandValue.AutoSize = true;
            this.labelCommandValue.Location = new System.Drawing.Point(40, 120);
            this.labelCommandValue.Name = "labelCommandValue";
            this.labelCommandValue.Size = new System.Drawing.Size(84, 23);
            this.labelCommandValue.TabIndex = 0;
            this.labelCommandValue.Text = "Value";
            this.labelCommandValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commandValueBox
            // 
            this.commandValueBox.Location = new System.Drawing.Point(60, 154);
            this.commandValueBox.Name = "commandValueBox";
            this.commandValueBox.Size = new System.Drawing.Size(543, 31);
            this.commandValueBox.TabIndex = 0;
            // 
            // CommandActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCommand);
            this.Controls.Add(this.commandSelectorBox);
            this.Controls.Add(this.labelCommandValue);
            this.Controls.Add(this.commandValueBox);
            this.Name = "CommandActionConfigView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCommand;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox commandSelectorBox;
        private System.Windows.Forms.Label labelCommandValue;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox commandValueBox;
    }
}