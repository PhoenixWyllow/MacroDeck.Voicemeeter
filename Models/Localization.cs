using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models
{
    internal sealed class Localization
    {
        public string Attribution { get; set; } = "built-in values";
        public string Language { get; set; } = "English (default)";
        public string PluginDescription { get; set; } = "A Voicemeeter plugin for Macro Deck";
        public string VoiceMeeterConnected { get; set; } = "Voicemeeter Connected";
        public string VoiceMeeterDisconnected { get; set; } = "Voicemeeter Disconnected";
    }
}
