using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PW.VoicemeeterPlugin.Views
{
    public partial class AddAdditionalVariablesConfigView : DialogForm
    {
        private int activeIndex;
        public AddAdditionalVariablesConfigView()
        {
            InitializeComponent();
            ApplyLocalization(); 

            var config = SuchByte.MacroDeck.Plugins.PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(AdditionalVariablesModel));
            var variables = string.IsNullOrEmpty(config) ? null : AdditionalVariablesModel.Deserialize(config);
            listParameters.Items.Clear();
            if (variables != null)
            {
                listParameters.Items.AddRange(variables.Options.ToArray());
            }
            variableType.Items.AddRange(Enum.GetNames(typeof(VariableType)));
            Reset();
        }

        private void ApplyLocalization()
        {
            labelParameter.Text = LocalizationManager.Instance.LabelParameter;
            labelVariableType.Text = LanguageManager.Strings.Type;
            buttonNew.Icon = SuchByte.MacroDeck.Properties.Resources.Create_Normal;
            ButtonSave.Icon = SuchByte.MacroDeck.Properties.Resources.Harddisk;
            buttonDelete.Icon = SuchByte.MacroDeck.Properties.Resources.Delete_Normal;
            ButtonOk.Text = LanguageManager.Strings.Ok;
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private bool CheckOption(VmIOOptions opt)
        {
            opt.Id = opt.Id.Replace('[', '(').Replace('{', '(').Replace(']', ')').Replace('}', ')');
            opt.Option = opt.Option.Split(';')[0];
            if (!int.TryParse(opt.Id.Substring(opt.Id.IndexOf('(')+1, 1), out _))
            {
                return false;
            }
            if (!PluginInstance.VoicemeeterControl.TryGetValue(opt.AsParameter, opt.Type, out _, infoOnly: true))
            {
                return false;
            }
            return true;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            //test parameter
            var param = parameterValue.Text.Split('.');
            var opt = new VmIOOptions() { Id = param[0], Option = string.Join('.', param[1..]), Type = Enum.Parse<VariableType>(variableType.Text) };
            if (!CheckOption(opt))
            {
                using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                _ = msgBox.ShowDialog(LanguageManager.Strings.Error, LocalizationManager.Instance.ParameterError, MessageBoxButtons.OK);
                return;
            }

            if (listParameters.Items.Contains(opt) || AvailableValues.Options.Contains(opt))
            {
                using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
                _ = msgBox.ShowDialog(LanguageManager.Strings.Info, LocalizationManager.Instance.ParameterExists, MessageBoxButtons.OK);
                return;
            }

            //not returned early - valid
            if (activeIndex >= 0)
            {
                listParameters.Items[activeIndex] = opt;
            }
            else
            {
                listParameters.Items.Add(opt);
            }
            Reset();
        }

        private void Reset()
        {
            parameterValue.Text = string.Empty;
            variableType.SelectedIndex = 0;
            activeIndex = -100;
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (activeIndex >= 0)
            {
                listParameters.Items.RemoveAt(activeIndex);
            }
            Reset();
        }

        private void ListParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeIndex = listParameters.SelectedIndex;
            if (activeIndex >= 0)
            {
                var opt = listParameters.Items[activeIndex] as VmIOOptions;
                parameterValue.Text = opt.AsParameter;
                variableType.SelectedItem = Enum.GetName(typeof(VariableType), opt.Type);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            AdditionalVariablesModel variables = new AdditionalVariablesModel
            {
                Options = new List<VmIOOptions>()
            };
            foreach (var item in listParameters.Items)
            {
                variables.Options.Add((VmIOOptions)item);
            }
            SuchByte.MacroDeck.Plugins.PluginConfiguration.SetValue(PluginInstance.Plugin, nameof(AdditionalVariablesModel), variables.Serialize());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
