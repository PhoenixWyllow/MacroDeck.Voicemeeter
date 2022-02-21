using AtgDev.Voicemeeter.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PW.MacroDeck.VoicemeeterPlugin.Models
{
    public class DeviceConfigModel : ISerializableConfiguration
    {
        public string Name { get; set; }
        public string Parameter { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static DeviceConfigModel Deserialize(string config)
        {
            return ISerializableConfiguration.Deserialize<DeviceConfigModel>(config);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
