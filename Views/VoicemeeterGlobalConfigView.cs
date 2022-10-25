using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;

namespace PW.VoicemeeterPlugin.Views
{
    public partial class VoicemeeterGlobalConfigView : DialogForm
    {
        private readonly VoicemeeterGlobalConfigViewModel _viewModel;
        public VoicemeeterGlobalConfigView(MacroDeckPlugin plugin)
        {
            _viewModel = new(plugin);
            InitializeComponent();
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
        }

        private void VoicemeeterGlobalConfigView_Load(object sender, EventArgs e)
        {
            checkBoxRunVoicemeeter.Checked = _viewModel.RunVoicemeeter;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            _viewModel.RunVoicemeeter = checkBoxRunVoicemeeter.Checked;
            _viewModel.SaveConfig();
        }
    }
}
