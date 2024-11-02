using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Linq;
using System.Windows.Forms;
using MessageBox = SuchByte.MacroDeck.GUI.CustomControls.MessageBox;

namespace PW.VoicemeeterPlugin.Views;

public partial class MacroButtonActionConfigView : ActionConfigControl
{
    private readonly MacroButtonActionConfigViewModel _viewModel;

    public MacroButtonActionConfigView(MacroButtonActionConfigViewModel viewModel)
    {
        _viewModel = viewModel;

        InitializeComponent();
        ApplyLocalization();

        buttonIdBox.Items.AddRange(Enumerable.Range(0, 80).Select(x => x.ToString()).ToArray());
        buttonTypeBox.Items.AddRange(Enum.GetNames<ButtonType>());
        buttonIdBox.SelectedIndex = _viewModel.ButtonId; // zero-based index and id
        buttonIdBox.SelectedIndex = (int)_viewModel.ButtonType;
    }

    private void ApplyLocalization()
    {
        labelButtonId.Text = LocalizationManager.Instance.MacroButtonButtonId;
        labelButtonType.Text = LocalizationManager.Instance.MacroButtonButtonType;
    }

    public override bool OnActionSave()
    {
        _viewModel.SaveConfig();

        if (!_viewModel.ValidConfig)
        {
            using var msgBox = new MessageBox();
            msgBox.ShowDialog(SuchByte.MacroDeck.Language.LanguageManager.Strings.Info, LocalizationManager.Instance.MacroButtonIdError, MessageBoxButtons.OK);
        }

        return _viewModel.ValidConfig;
    }

    private void DeviceSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.ButtonId = buttonIdBox.SelectedIndex;
    }

    private void TypeSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.ButtonType = (ButtonType)buttonTypeBox.SelectedIndex;
    }

}