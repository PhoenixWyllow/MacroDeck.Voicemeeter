using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Variables;
using System;
using System.Windows.Forms;

namespace PW.VoicemeeterPlugin.Views;

public partial class AddAdditionalVariablesConfigView : DialogForm
{
    private int _activeIndex;
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

    private bool CheckOption(ReadOnlySpan<char> id, string option, ReadOnlySpan<char> typeStr, out VmIoOptions opt)
    {
        Span<char> optId = stackalloc char[id.Length];
        for (int i = 0; i < id.Length; i++)
        {
            optId[i] = id[i] switch
            {
                '[' or '{' => '(',
                ']' or '}' => ')',
                _ => id[i],
            };
        }
        opt = new(optId.ToString(), option.Split(';')[0], Enum.Parse<VariableType>(typeStr));
        int idxBracket = opt.Id.IndexOf('(');
        if (idxBracket < -1 && !int.TryParse(optId[idxBracket + 1].ToString(), out _))
        {
            return false;
        }
        return PluginInstance.VoicemeeterControl.TryGetValue(opt.AsParameter, opt.Type, out _, infoOnly: true);
    }

    private void ButtonSave_Click(object sender, EventArgs e)
    {
        //test parameter
        var param = parameterValue.Text.Split('.');
        if (!CheckOption(param[0], string.Join('.', param[1..]), variableType.Text, out var opt))
        {
            using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            _ = msgBox.ShowDialog(LanguageManager.Strings.Error, LocalizationManager.Instance.ParameterError, MessageBoxButtons.OK);
            return;
        }

        if (listParameters.Items.Contains(opt) || (AvailableValues.IoOptions?.Contains(opt) ?? false))
        {
            using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            _ = msgBox.ShowDialog(LanguageManager.Strings.Info, LocalizationManager.Instance.ParameterExists, MessageBoxButtons.OK);
            return;
        }

        //not returned early - valid
        if (_activeIndex >= 0)
        {
            listParameters.Items[_activeIndex] = opt;
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
        _activeIndex = -100;
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (_activeIndex >= 0)
        {
            listParameters.Items.RemoveAt(_activeIndex);
        }
        Reset();
    }

    private void ListParameters_SelectedIndexChanged(object sender, EventArgs e)
    {
        _activeIndex = listParameters.SelectedIndex;
        if (_activeIndex >= 0)
        {
            var opt = (VmIoOptions)listParameters.Items[_activeIndex];
            parameterValue.Text = opt.AsParameter;
            variableType.SelectedItem = Enum.GetName(typeof(VariableType), opt.Type)!;
        }
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        AdditionalVariablesModel variables = new()
        {
            Options = new()
        };
        foreach (var item in listParameters.Items)
        {
            variables.Options.Add((VmIoOptions)item);
        }
        SuchByte.MacroDeck.Plugins.PluginConfiguration.SetValue(PluginInstance.Plugin, nameof(AdditionalVariablesModel), variables.Serialize());

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

}