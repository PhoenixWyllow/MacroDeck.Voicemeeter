using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Linq;
using System.Windows.Forms;
using SuchByte.MacroDeck.Language;

namespace PW.VoicemeeterPlugin.Views;

public partial class DeviceSelectorConfigView : ActionConfigControl
{
    private readonly DeviceSelectorViewModel _viewModel;

    public DeviceSelectorConfigView(DeviceSelectorViewModel viewModel)
    {
        _viewModel = viewModel;

        InitializeComponent();
        ApplyLocalization();

        actionSliderValue.Visible = _viewModel.IsSlider;
        if (_viewModel.IsSlider)
        {
            actionSliderValue.Value = (decimal)_viewModel.SliderValue;
            actionSliderValue.ValueChanged += (s, _) => _viewModel.SliderValue = (float)((NumericUpDown)s!).Value;
        }
        deviceSelectorBox.Items.AddRange(_viewModel.AvailableDevices?.ToArray() ?? Array.Empty<VmIoInfo>());
        if (_viewModel.SelectedDevice != null)
        {
            deviceSelectorBox.SelectedItem = _viewModel.SelectedDevice;
        }
        if (_viewModel.SelectedAction != null)
        {
            actionSelectorBox.SelectedItem = _viewModel.SelectedAction;
        }
    }

    private void ApplyLocalization()
    {
        labelAction.Text = LocalizationManager.Instance.Action;
        labelDevice.Text = LocalizationManager.Instance.Device;
        labelSlider.Text = LocalizationManager.Instance.SliderValue;
    }

    public override bool OnActionSave()
    {
        if (_viewModel.IsSlider && _viewModel.SliderValue == 0)
        {
            using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            _ = msgBox.ShowDialog(LanguageManager.Strings.Error, LocalizationManager.Instance.ErrorZeroSliderValue, MessageBoxButtons.OK);
            return false;
        }

        if (_viewModel.SelectedDevice is null)
        {
            using var msgBox = new SuchByte.MacroDeck.GUI.CustomControls.MessageBox();
            _ = msgBox.ShowDialog(LanguageManager.Strings.Error, LocalizationManager.Instance.ErrorNoDeviceSelected, MessageBoxButtons.OK);
            return false;
        }
        _viewModel.SaveConfig();

        return base.OnActionSave();
    }

    private void DeviceSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.ChangeDevice((VmIoInfo)deviceSelectorBox.SelectedItem);
        actionSelectorBox.Items.Clear();
        actionSelectorBox.Items.AddRange(_viewModel.AvailableActions);
    }

    private void ActionSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.ChangeAction((string)actionSelectorBox.SelectedItem);
    }
}