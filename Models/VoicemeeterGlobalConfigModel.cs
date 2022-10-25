using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models
{

    public sealed class VoicemeeterGlobalConfigModel : ISerializableConfiguration
    {
        public bool RunVoicemeeter { get; set; }
        public bool TryReconnectOnError { get; set; } = true;

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static VoicemeeterGlobalConfigModel Deserialize(string config)
        {
            return ISerializableConfiguration.Deserialize<VoicemeeterGlobalConfigModel>(config);
        }
    }
}
