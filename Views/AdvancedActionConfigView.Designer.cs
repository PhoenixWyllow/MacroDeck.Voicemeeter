namespace PW.VoicemeeterPlugin.Views
{
    partial class AdvancedActionConfigView
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
            this.commandsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // commandsBox
            // 
            this.commandsBox.Location = new System.Drawing.Point(3, 65);
            this.commandsBox.Multiline = true;
            this.commandsBox.Name = "commandsBox";
            this.commandsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commandsBox.Size = new System.Drawing.Size(400, 356);
            this.commandsBox.TabIndex = 0;
            // 
            // AdvancedActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.commandsBox);
            this.Name = "AdvancedActionConfigView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commandsBox;
    }
}