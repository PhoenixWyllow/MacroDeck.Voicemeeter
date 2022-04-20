using AtgDev.Voicemeeter.Types;
using PW.VoicemeeterPlugin;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter
{
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

        public static List<VmIOInfo> IOInfo { get; private set; }

        internal static void InitIOInfo(AtgDev.Voicemeeter.RemoteApiExtender vmrApi)
        {
            void AddChannel(VmIOType type, int num)
            {
                try
                {
                    VmIOInfo channel = new VmIOInfo
                    {
                        Id = $"{type}({num})",
                        Type = type,
                        IsPhysical = num < (type == VmIOType.Strip ? MaxPhysicalStrips : MaxPhysicalBuses),
                    };
                    ControlHelpers.TestResultThrow(vmrApi.GetParameter($"{channel.Id}.Label", out string label));
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
                for (int i = 0; i < MaxStrips; i++)
                {
                    AddChannel(VmIOType.Strip, i);
                }

                for (int i = 0; i < MaxBuses; i++)
                {
                    AddChannel(VmIOType.Bus, i);
                }

            }
        }

        public static List<VmIOOptions> Options { get; private set; }

        internal static void InitOptions()
        {
            if (Options is null)
            {
                Options = new List<VmIOOptions>();
                foreach (var channel in IOInfo)
                {
                    string channelId = channel.Id;
                    Options.Add(new VmIOOptions { Id = channelId, Option = "Mute", Type = VariableType.Bool });
                    Options.Add(new VmIOOptions { Id = channelId, Option = "Mono", Type = VariableType.Bool });
                    Options.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
                    if (channel.Type == VmIOType.Strip)
                    {
                        if (channel.IsPhysical)
                        {
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Solo", Type = VariableType.Bool });
                        }

                        int maxPhysical = MaxPhysicalBuses;
                        for (int i = 1; i <= MaxBuses; i++)
                        {
                            string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                            Options.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                        }
                    }
                    else
                    {
                        if (ConnectedType == VoicemeeterType.Banana || ConnectedType == VoicemeeterType.Potato)
                        {
                            Options.Add(new VmIOOptions { Id = channelId, Option = "EQ.on", Type = VariableType.Bool });

                            if (ConnectedType == VoicemeeterType.Potato)
                            {
                                Options.Add(new VmIOOptions { Id = channelId, Option = "Sel", Type = VariableType.Bool });
                            }
                        }
                    }
                }
            }
        }

    }
}
