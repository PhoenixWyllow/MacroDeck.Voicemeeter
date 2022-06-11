using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PW.VoicemeeterPlugin.Views
{
    public partial class DeviceSelectorConfigView : ActionConfigControl
    {
        private readonly DeviceSelectorViewModel _viewModel;

        public DeviceSelectorConfigView(DeviceSelectorViewModel viewModel)
        {
            _viewModel = viewModel;

            InitializeComponent();
            ApplyLocalization();

            deviceSelectorBox.Items.AddRange(_viewModel.AvailableDevices.ToArray());
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
        }

        public override bool OnActionSave()
        {
            _viewModel.SaveConfig();

            return base.OnActionSave();
        }

        private void DeviceSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.ChangeDevice((VmIOInfo)deviceSelectorBox.SelectedItem);
            actionSelectorBox.Items.Clear();
            actionSelectorBox.Items.AddRange(_viewModel.AvailableActions);
        }

        private void ActionSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.ChangeAction((string)actionSelectorBox.SelectedItem);
        }
    }
}
