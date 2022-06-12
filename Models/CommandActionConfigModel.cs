using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class CommandActionConfigModel : ISerializableConfiguration
    {
        public VmIOCommand Command { get; set; } = new VmIOCommand();
        public string CommandValue { get; set; } = string.Empty;

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
        public static CommandActionConfigModel Deserialize(string config)
        {
            return ISerializableConfiguration.Deserialize<CommandActionConfigModel>(config);
        }

        public override string ToString()
        {
            string val = Command.ToString();
            if (!string.IsNullOrWhiteSpace(CommandValue))
            {
                val += " = " + CommandValue;
            }
            return val;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
