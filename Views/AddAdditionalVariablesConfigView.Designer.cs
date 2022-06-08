using SuchByte.MacroDeck.GUI.CustomControls;

namespace PW.VoicemeeterPlugin.Views
{
    partial class AddAdditionalVariablesConfigView
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
            this.labelParameter = new System.Windows.Forms.Label();
            this.labelVariableType = new System.Windows.Forms.Label();
            this.listParameters = new System.Windows.Forms.ListBox();
            this.parameterValue = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.variableType = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.ButtonSave = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.buttonDelete = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.buttonNew = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.ButtonOk = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.SuspendLayout();
            // 
            // labelParameter
            // 
            this.labelParameter.Location = new System.Drawing.Point(12, 50);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(140, 23);
            this.labelParameter.TabIndex = 4;
            this.labelParameter.Text = "Parameter";
            this.labelParameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelParameter.UseMnemonic = false;
            // 
            // labelVariableType
            // 
            this.labelVariableType.Location = new System.Drawing.Point(12, 103);
            this.labelVariableType.Name = "labelVariableType";
            this.labelVariableType.Size = new System.Drawing.Size(105, 23);
            this.labelVariableType.TabIndex = 4;
            this.labelVariableType.Text = "Type";
            this.labelVariableType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelVariableType.UseMnemonic = false;
            // 
            // listParameters
            // 
            this.listParameters.FormattingEnabled = true;
            this.listParameters.ItemHeight = 25;
            this.listParameters.Location = new System.Drawing.Point(12, 148);
            this.listParameters.Name = "listParameters";
            this.listParameters.Size = new System.Drawing.Size(474, 204);
            this.listParameters.TabIndex = 7;
            this.listParameters.SelectedIndexChanged += ListParameters_SelectedIndexChanged;
            // 
            // parameterValue
            // 
            this.parameterValue.Location = new System.Drawing.Point(155, 46);
            this.parameterValue.Name = "parameterValue";
            this.parameterValue.Size = new System.Drawing.Size(253, 31);
            this.parameterValue.TabIndex = 8;
            // 
            // variableType
            // 
            this.variableType.Location = new System.Drawing.Point(115, 98);
            this.variableType.Name = "variableType";
            this.variableType.Size = new System.Drawing.Size(182, 33);
            this.variableType.TabIndex = 9;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(412, 98);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(34, 34);
            this.ButtonSave.TabIndex = 10;
            this.ButtonSave.Text = "";
            this.ButtonSave.ForeColor = System.Drawing.Color.White;
            this.ButtonSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.ButtonSave.Icon = null;
            this.ButtonSave.Progress = 0;
            this.ButtonSave.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.UseWindowsAccentColor = true;
            this.ButtonSave.Click += ButtonSave_Click;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(452, 98);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(34, 34);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "";
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.buttonDelete.Icon = null;
            this.buttonDelete.Progress = 0;
            this.buttonDelete.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.UseWindowsAccentColor = true;
            this.buttonDelete.Click += ButtonDelete_Click;
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(372, 98);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(34, 34);
            this.buttonNew.TabIndex = 12;
            this.buttonNew.Text = "";
            this.buttonNew.ForeColor = System.Drawing.Color.White;
            this.buttonNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.buttonNew.Icon = null;
            this.buttonNew.Progress = 0;
            this.buttonNew.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.UseWindowsAccentColor = true;
            this.buttonNew.Click += ButtonNew_Click;
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(374, 365);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(112, 34);
            this.ButtonOk.TabIndex = 13;
            this.ButtonOk.Text = "Ok";
            this.ButtonOk.ForeColor = System.Drawing.Color.White;
            this.ButtonOk.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.ButtonOk.Icon = null;
            this.ButtonOk.Progress = 0;
            this.ButtonOk.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.UseWindowsAccentColor = true;
            this.ButtonOk.Click += ButtonOk_Click;
            // 
            // AddAdditionalVariablesConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(498, 411);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.variableType);
            this.Controls.Add(this.parameterValue);
            this.Controls.Add(this.labelParameter);
            this.Controls.Add(this.labelVariableType);
            this.Controls.Add(this.listParameters);
            this.Name = "AddAdditionalVariablesConfigView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.Label labelVariableType;
        private System.Windows.Forms.ListBox listParameters;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox parameterValue;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox variableType;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary ButtonSave;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonDelete;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary buttonNew;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary ButtonOk;
    }
}