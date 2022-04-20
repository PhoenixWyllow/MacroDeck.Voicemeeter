using AtgDev.Voicemeeter.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class DeviceConfigModel : ISerializableConfiguration
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public VmIOOptions Option { get; set; }
        public float Value { get; set; } = 0;

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
            string value = Value == 0 ? string.Empty : Value > 0 ? " +" + Value.ToString() : " -" + Value.ToString();
            return Name + ": " + Action + value;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
