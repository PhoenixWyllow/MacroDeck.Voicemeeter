namespace PW.VoicemeeterPlugin.Views
{
    partial class VoicemeeterGlobalConfigView
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
            this.buttonOK = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.checkBoxRunVoicemeeter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BorderRadius = 8;
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.buttonOK.Icon = null;
            this.buttonOK.Location = new System.Drawing.Point(367, 141);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Progress = 0;
            this.buttonOK.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.buttonOK.Size = new System.Drawing.Size(75, 27);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.UseWindowsAccentColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // checkBoxRunVoicemeeter
            // 
            this.checkBoxRunVoicemeeter.AutoSize = true;
            this.checkBoxRunVoicemeeter.Location = new System.Drawing.Point(31, 71);
            this.checkBoxRunVoicemeeter.Name = "checkBoxRunVoicemeeter";
            this.checkBoxRunVoicemeeter.Size = new System.Drawing.Size(239, 20);
            this.checkBoxRunVoicemeeter.TabIndex = 6;
            this.checkBoxRunVoicemeeter.Text = "Run Voicemeeter at Macro Deck start";
            this.checkBoxRunVoicemeeter.UseVisualStyleBackColor = true;
            // 
            // VoicemeeterGlobalConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 172);
            this.Controls.Add(this.checkBoxRunVoicemeeter);
            this.Controls.Add(this.buttonOK);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "VoicemeeterGlobalConfigView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.VoicemeeterGlobalConfigView_Load);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.checkBoxRunVoicemeeter, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonOK;
        private System.Windows.Forms.CheckBox checkBoxRunVoicemeeter;
    }
}