using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PW.VoicemeeterPlugin.Models
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class VmIOOptions
    {
        public string Id { get; set; }
        public string Option { get; set; }
        public VariableType Type { get; set; }

        public string AsParameter => $"{Id}.{Option}";
        public string AsVariable => $"vm_{Id}_{Option}".ToLower();

        private string GetDebuggerDisplay()
        {
            return $"{Type}: {AsParameter}";
        }
    }
}
