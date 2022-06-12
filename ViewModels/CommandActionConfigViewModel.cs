using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.VoicemeeterPlugin.ViewModels
{
    public class CommandActionConfigViewModel : ISavableConfigViewModel
    {
        private readonly PluginAction _action;

        private readonly CommandActionConfigModel configuration;

        ISerializableConfiguration ISavableConfigViewModel.SerializableConfiguration => configuration;

        public CommandActionConfigViewModel(PluginAction action)
        {
            _action = action;
            configuration = CommandActionConfigModel.Deserialize(action.Configuration);
            if (configuration.Command != null)
            {
                ChangeCommand(configuration.Command);
                CommandValue = configuration.CommandValue;
            }
        }
        public IEnumerable<VmIOCommand> AvailableCommands { get; } = AvailableValues.AvailableCommands;
        public VmIOCommand SelectedCommand { get; private set; } = new VmIOCommand();
        public string CommandValue { get; set; }

        public void ChangeCommand(VmIOCommand selectedCommand)
        {
            if (selectedCommand.CommandType is null)
            {
                return;
            }
            SelectedCommand = AvailableCommands.FirstOrDefault(command => command.Equals(selectedCommand));
            if (!SelectedCommand.RequiresValue)
            {
                CommandValue = string.Empty;
            }
        }

        public void SetConfig()
        {
            ValidConfig = !(SelectedCommand.CommandType is null) || !SelectedCommand.RequiresValue || !string.IsNullOrWhiteSpace(CommandValue);
            if (!ValidConfig)
            {
                return;
            }
            configuration.Command = SelectedCommand;
            configuration.CommandValue = CommandValue;

            _action.ConfigurationSummary = configuration.ToString();
            _action.Configuration = configuration.Serialize();
        }

        public void SaveConfig()
        {
            try
            {
                SetConfig();
                MacroDeckLogger.Info(PluginInstance.Plugin, $"{GetType().Name}: config saved");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Plugin, $"{GetType().Name}: config NOT saved");
                MacroDeckLogger.Error(PluginInstance.Plugin, $"{GetType().Name}: {ex.Message}");
            }
        }

        public bool ValidConfig { get; private set; }
    }
}
