using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Linq;
using System.Windows.Forms;
using MessageBox = SuchByte.MacroDeck.GUI.CustomControls.MessageBox;

namespace PW.VoicemeeterPlugin.Views
{
    public partial class CommandActionConfigView : ActionConfigControl
    {
        private readonly CommandActionConfigViewModel _viewModel;

        public CommandActionConfigView(CommandActionConfigViewModel viewModel)
        {
            _viewModel = viewModel;

            InitializeComponent();
            ApplyLocalization();

            commandSelectorBox.Items.AddRange(_viewModel.AvailableCommands.ToArray());
            if (_viewModel.SelectedCommand != null)
            {
                commandSelectorBox.SelectedItem = _viewModel.SelectedCommand;
            }
            commandValueBox.Text = _viewModel.CommandValue;
        }

        private void ApplyLocalization()
        {
            labelCommand.Text = LocalizationManager.Instance.Command;
            labelCommandValue.Text = LocalizationManager.Instance.CommandValue;
        }

        public override bool OnActionSave()
        {
            _viewModel.CommandValue = commandValueBox.Text;
            _viewModel.SaveConfig();

            if (!_viewModel.ValidConfig)
            {
                using var msgBox = new MessageBox();
                msgBox.ShowDialog(SuchByte.MacroDeck.Language.LanguageManager.Strings.Info, LocalizationManager.Instance.CommandError, MessageBoxButtons.OK);
            }

            return _viewModel.ValidConfig;
        }

        private void DeviceSelectorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var command = (VmIoCommand)commandSelectorBox.SelectedItem;
            _viewModel.ChangeCommand(command);
            commandValueBox.Enabled = command.RequiresValue;
        }

    }
}
