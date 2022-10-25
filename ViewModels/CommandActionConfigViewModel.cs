using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PW.VoicemeeterPlugin.ViewModels
{
    public class CommandActionConfigViewModel : ISavableConfigViewModel
    {
        private readonly PluginAction _action;

        private readonly CommandActionConfigModel _configuration;

        ISerializableConfiguration ISavableConfigViewModel.SerializableConfiguration => _configuration;

        public CommandActionConfigViewModel(PluginAction action)
        {
            _action = action;
            _configuration = CommandActionConfigModel.Deserialize(action.Configuration);
            if (_configuration.Command != null)
            {
                ChangeCommand(_configuration.Command);
                CommandValue = _configuration.CommandValue;
            }
        }
        public IEnumerable<VmIoCommand> AvailableCommands { get; } = AvailableValues.IoCommands;
        public VmIoCommand SelectedCommand { get; private set; } = new();
        public string CommandValue { get; set; }

        public void ChangeCommand(VmIoCommand selectedCommand)
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
            ValidConfig = SelectedCommand.CommandType is not null || !SelectedCommand.RequiresValue || !string.IsNullOrWhiteSpace(CommandValue);
            if (!ValidConfig)
            {
                return;
            }
            _configuration.Command = SelectedCommand;
            _configuration.CommandValue = CommandValue;

            _action.ConfigurationSummary = _configuration.ToString();
            _action.Configuration = _configuration.Serialize();
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
