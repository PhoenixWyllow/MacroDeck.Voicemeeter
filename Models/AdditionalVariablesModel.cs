using AtgDev.Voicemeeter.Types;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class AdditionalVariablesModel : ISerializableConfiguration
    {
        public List<VmIOOptions> Options { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static AdditionalVariablesModel Deserialize(string config)
        {
            return ISerializableConfiguration.Deserialize<AdditionalVariablesModel>(config);
        }

        public override string ToString()
        {
            return Options.ToString();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
