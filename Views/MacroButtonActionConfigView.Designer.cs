using System;

namespace PW.VoicemeeterPlugin.Views
{
    partial class MacroButtonActionConfigView
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
            this.labelButtonId = new System.Windows.Forms.Label();
            this.buttonIdBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.labelButtonType = new System.Windows.Forms.Label();
            this.buttonTypeBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.SuspendLayout();
            // 
            // labelButtonId
            // 
            this.labelButtonId.AutoSize = true;
            this.labelButtonId.Location = new System.Drawing.Point(40, 45);
            this.labelButtonId.Name = "labelButtonId";
            this.labelButtonId.Size = new System.Drawing.Size(84, 23);
            this.labelButtonId.TabIndex = 0;
            this.labelButtonId.Text = "ButtonId";
            this.labelButtonId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonIdBox
            // 
            this.buttonIdBox.Location = new System.Drawing.Point(60, 79);
            this.buttonIdBox.Name = "buttonIdBox";
            this.buttonIdBox.Size = new System.Drawing.Size(543, 31);
            this.buttonIdBox.TabIndex = 0;
            this.buttonIdBox.SelectedIndexChanged += DeviceSelectorBox_SelectedIndexChanged;
            // 
            // labelButtonType
            // 
            this.labelButtonType.AutoSize = true;
            this.labelButtonType.Location = new System.Drawing.Point(40, 120);
            this.labelButtonType.Name = "labelButtonType";
            this.labelButtonType.Size = new System.Drawing.Size(84, 23);
            this.labelButtonType.TabIndex = 0;
            this.labelButtonType.Text = "ButtonType";
            this.labelButtonType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonTypeBox
            // 
            this.buttonTypeBox.Location = new System.Drawing.Point(60, 154);
            this.buttonTypeBox.Name = "buttonTypeBox";
            this.buttonTypeBox.Size = new System.Drawing.Size(543, 31);
            this.buttonTypeBox.TabIndex = 0;
            this.buttonTypeBox.SelectedIndexChanged += TypeSelectorBox_SelectedIndexChanged;
            // 
            // MacroButtonActionConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelButtonId);
            this.Controls.Add(this.buttonIdBox);
            this.Controls.Add(this.labelButtonType);
            this.Controls.Add(this.buttonTypeBox);
            this.Name = "MacroButtonActionConfigView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelButtonId;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox buttonIdBox;
        private System.Windows.Forms.Label labelButtonType;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox buttonTypeBox;
    }
}