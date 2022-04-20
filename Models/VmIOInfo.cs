using System;
using System.Collections.Generic;
using System.Text;

namespace PW.VoicemeeterPlugin.Models
{
    public class VmIOInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public VmIOType Type { get; set; }
        public bool IsPhysical { get; set; }
    }

    public enum VmIOType { Strip, Bus }
}
