using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.Models
{
    public class VmIOOptions
    {
        public string Id { get; set; }
        public string Option { get; set; }
        public VariableType Type { get; set; }

        public string AsParameter => $"{Id}.{Option}";
        public string AsVariable => $"vm_{AsParameter.Replace('(','_').Replace(')','_').Replace('.', '_').ToLower()}";
    }
}
