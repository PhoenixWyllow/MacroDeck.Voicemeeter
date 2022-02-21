using PW.MacroDeck.VoicemeeterPlugin.Actions;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PW.MacroDeck.VoicemeeterPlugin.Views
{
    public partial class AdvancedActionConfigView : ActionConfigControl
    {
        private readonly AdvancedAction _action;

        public AdvancedActionConfigView(AdvancedAction action)
        {
            _action = action;
            InitializeComponent();

            commandsBox.Text = action.Configuration;
        }

        public override bool OnActionSave()
        {
            _action.Configuration = commandsBox.Text;
            var commands = _action.Configuration.Split(new string[] { Environment.NewLine, ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            _action.ConfigurationSummary = commands.Length switch
            {
                0 => "^no commands no execute",
                1 => commands[0],
                _ => string.Format("^{0} commands", commands.Length),
            };
            return true;
        }
    }
}
