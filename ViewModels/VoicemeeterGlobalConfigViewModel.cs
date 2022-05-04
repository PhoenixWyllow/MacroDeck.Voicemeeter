using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.ViewModels
{
    public class VoicemeeterGlobalConfigViewModel : ISavableConfigViewModel
    {
        private readonly MacroDeckPlugin _plugin;
        private VoicemeeterGlobalConfigModel OutputConfiguration { get; }

        public VoicemeeterGlobalConfigViewModel(MacroDeckPlugin plugin)
        {
            _plugin = plugin;

            OutputConfiguration = VoicemeeterGlobalConfigModel.Deserialize(PluginConfiguration.GetValue(plugin, nameof(VoicemeeterGlobalConfigModel)));
        }

        ISerializableConfiguration ISavableConfigViewModel.SerializableConfiguration => OutputConfiguration;

        public bool RunVoicemeeter { get => OutputConfiguration.RunVoicemeeter; set => OutputConfiguration.RunVoicemeeter = value; }

        public bool TryReconnectOnError { get => OutputConfiguration.TryReconnectOnError; set => OutputConfiguration.TryReconnectOnError = value; }

        public void SaveConfig()
        {
            try
            {
                SetConfig();
                MacroDeckLogger.Info(_plugin, $"{GetType().Name}: config saved");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(_plugin, $"{GetType().Name}: config NOT saved");
                MacroDeckLogger.Error(_plugin, $"{GetType().Name}: {ex.Message}");
            }
        }

        public void SetConfig()
        {
            PluginConfiguration.SetValue(_plugin, nameof(VoicemeeterGlobalConfigModel), OutputConfiguration.Serialize());
        }
    }
}
