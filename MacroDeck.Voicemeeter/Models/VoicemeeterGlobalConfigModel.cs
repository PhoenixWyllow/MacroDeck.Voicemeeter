using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PW.MacroDeck.VoicemeeterPlugin.Models
{
    
    public class VoicemeeterGlobalConfigModel : ISerializableConfiguration
    {
        public bool RunVoicemeeter { get; set; }

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
