using SuchByte.MacroDeck.Variables;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class VmIOOptions
    {
        public string Id { get; set; }
        public string Option { get; set; }
        public VariableType Type { get; set; }

        [JsonIgnore]
        public string AsParameter => $"{Id}.{Option}";

        [JsonIgnore]
        public string AsVariable => VariableManager.ConvertNameString($"vm_{Id.Replace("(", null).Replace(")", null)}_{Option}");

        private string GetDebuggerDisplay()
        {
            return $"{Type}: {AsParameter}";
        }
    }
}
