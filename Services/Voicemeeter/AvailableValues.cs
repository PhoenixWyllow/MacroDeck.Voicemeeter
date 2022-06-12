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
            VoicemeeterType.Potato64 => 8,
            _ => 0,
        };

        public static int MaxBuses => ConnectedType switch
        {
            VoicemeeterType.Standard => 2,
            VoicemeeterType.Banana => 5,
            VoicemeeterType.Potato => 8,
            VoicemeeterType.Potato64 => 8,
            _ => 0,
        };

        public static int MaxPhysicalStrips => ConnectedType switch
        {
            VoicemeeterType.Standard => 2,
            VoicemeeterType.Banana => 3,
            VoicemeeterType.Potato => 5,
            VoicemeeterType.Potato64 => 5,
            _ => 0,
        };

        public static int MaxPhysicalBuses => ConnectedType switch
        {
            VoicemeeterType.Standard => 1,
            VoicemeeterType.Banana => 3,
            VoicemeeterType.Potato => 5,
            VoicemeeterType.Potato64 => 5,
            _ => 0,
        };

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


                IOInfo.Add(new VmIOInfo
                {
                    Id = nameof(VmIOType.Recorder),
                    Type = VmIOType.Recorder,
                    IsPhysical = false,
                });
            }
        }

        public static List<VmIOOptions> Options { get; private set; }

        internal static void InitOptions()
        {
            if (Options is null)
            {
                int maxPhysical = MaxPhysicalBuses;
                Options = new List<VmIOOptions>();
                foreach (var channel in IOInfo)
                {
                    string channelId = channel.Id;
                    if (channel.Type != VmIOType.Recorder)
                    {
                        Options.Add(new VmIOOptions { Id = channelId, Option = "Mute", Type = VariableType.Bool });
                        Options.Add(new VmIOOptions { Id = channelId, Option = "Mono", Type = VariableType.Bool });
                        Options.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
                        Options.Add(new VmIOOptions { Id = channelId, Option = "Label", Type = VariableType.String });
                        if (channel.Type == VmIOType.Strip)
                        {
                            if (channel.IsPhysical)
                            {
                                Options.Add(new VmIOOptions { Id = channelId, Option = "Solo", Type = VariableType.Bool });
                            }

                            for (int i = 1; i <= MaxBuses; i++)
                            {
                                string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                                Options.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                            }
                        }
                        else
                        {
                            if (IsBananaOrPotato)
                            {
                                Options.Add(new VmIOOptions { Id = channelId, Option = "EQ.on", Type = VariableType.Bool });

                                if (ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64)
                                {
                                    Options.Add(new VmIOOptions { Id = channelId, Option = "Sel", Type = VariableType.Bool });
                                }
                            }
                        }
                    }
                    else
                    {
                        //Add Recorder options
                        if (IsBananaOrPotato)
                        {
                            for (int i = 1; i <= MaxBuses; i++)
                            {
                                string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                                Options.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                            }
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Stop", Type = VariableType.Bool });
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Play", Type = VariableType.Bool });
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Record", Type = VariableType.Bool });
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Pause", Type = VariableType.Bool });
                            Options.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
                        }
                    }
                }

                //Additional variables
                var config = SuchByte.MacroDeck.Plugins.PluginConfiguration.GetValue(PluginInstance.Plugin, nameof(AdditionalVariablesModel));
                var variables = string.IsNullOrEmpty(config) ? null : AdditionalVariablesModel.Deserialize(config);
                if (variables != null)
                {
                    foreach (var addedVariable in variables.Options)
                    {
                        if (!Options.Contains(addedVariable))
                        {
                            Options.Add(addedVariable);
                        }
                    }
                }
            }
        }

        private static bool IsBananaOrPotato => ConnectedType == VoicemeeterType.Banana || ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64;

        public static List<VmIOCommand> AvailableCommands { get; } = FetchCommands();
        private static List<VmIOCommand> FetchCommands()
        {
            var availableCommands = new List<VmIOCommand>();
            var commands = Enum.GetValues(typeof(Commands));
            foreach (Commands command in commands)
            {
                availableCommands.Add(new VmIOCommand()
                {
                    CommandType = command,
                    RequiresValue = command.Equals(Commands.ConfigLoad) || command.Equals(Commands.ConfigSave) || command.Equals(Commands.RecorderLoad),
                });
            }
            return availableCommands;
        }
    }
}
