using AtgDev.Voicemeeter.Types;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.MacroDeck.VoicemeeterPlugin.Models
{
    enum Type { Strip, Bus }
    
    internal static class AvailableValues
    {
        public static VoicemeeterType ConnectedType { get; set; }
        public static int MaxStrips => ConnectedType switch
        {
            VoicemeeterType.Standard => 3,
            VoicemeeterType.Banana => 5,
            VoicemeeterType.Potato => 8,
            _ => 0,
        };

        public static int MaxBuses => ConnectedType switch
        {
            VoicemeeterType.Standard => 2,
            VoicemeeterType.Banana => 5,
            VoicemeeterType.Potato => 8,
            _ => 0,
        };

        public static int MaxPhysicalStrips => MaxStrips - (int)ConnectedType;
        public static int MaxPhysicalBuses => MaxBuses - (int)ConnectedType;

        public static List<string> StripToggles => GetStripToggles();

        private static List<string> GetStripToggles()
        {
            //List<string> vs = from _op
            List<string> toggles = new List<string>
            {
                "Mute",
                "A1",
                "B1"
            };
            
            if (ConnectedType == VoicemeeterType.Banana || ConnectedType == VoicemeeterType.Potato)
            {
                toggles.Add("A2");
                toggles.Add("A3");
                toggles.Add("B2");

                if (ConnectedType == VoicemeeterType.Potato)
                {
                    toggles.Add("A4");
                    toggles.Add("A5");
                    toggles.Add("B3");
                }
            }

            toggles.Sort();
            return toggles;
        }

        public static List<VmIOInfo> IOInfo { get; private set; }

        internal static void InitIOInfo(AtgDev.Voicemeeter.RemoteApiExtender vmrApi)
        {
            void AddChannelInfo(VmIOInfo channel)
            {
                try
                {
                    Services.Voicemeeter.VoicemeeterControlHelpers.TestResultThrow(vmrApi.GetParameter($"{channel.Id}.Label", out string label));
                    channel.Name = label;
                    IOInfo.Add(channel);
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Trace(PluginInstance.Plugin, nameof(InitIOInfo) + ": " + ex.Message);
                }
            }

            if (IOInfo is null)
            {
                IOInfo = new List<VmIOInfo>();
                for (int i = 0; i < AvailableValues.MaxStrips; i++)
                {
                    AddChannelInfo(new VmIOInfo { Id = $"Strip({i})" });
                }

                for (int i = 0; i < AvailableValues.MaxBuses; i++)
                {
                    AddChannelInfo(new VmIOInfo { Id = $"Bus({i})" });
                }

            }
        }

        public static List<VmIOOptions> Options { get; private set; }

        internal static void InitOptions()
        {
            if (Options is null)
            {
                Options = new List<VmIOOptions>();
                foreach (string channelId in IOInfo.Select(c => c.Id))
                {
                    Options.Add(new VmIOOptions { Id = channelId, Option = "Mute", Type = VariableType.Bool });
                    Options.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
                    if (channelId.StartsWith("Strip"))
                    {
                        int maxPhysical = MaxPhysicalBuses;
                        for (int i = 1; i <= MaxBuses; i++)
                        {
                            string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                            Options.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                        }
                    }
                    //else if (channelId.StartsWith("Bus"))
                    //{

                    //}
                }
            }
        }

    }
}
