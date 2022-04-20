using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PW.VoicemeeterPlugin.Views
{
    public partial class VoicemeeterGlobalConfigView : DialogForm
    {
        private readonly VoicemeeterGlobalConfigViewModel _viewModel;
        public VoicemeeterGlobalConfigView(MacroDeckPlugin plugin)
        {
            _viewModel = new VoicemeeterGlobalConfigViewModel(plugin);
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

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            _viewModel.RunVoicemeeter = checkBoxRunVoicemeeter.Checked;
            _viewModel.SaveConfig();
        }
    }
}
