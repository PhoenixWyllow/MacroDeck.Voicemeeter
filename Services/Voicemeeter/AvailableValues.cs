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

        internal static void Reset()
        {
            IOInfo = null;
            IOOptions = null;
            IOCommands = null;
        }

        public static List<VmIOInfo> IOInfo { get; private set; }

        internal static void InitIOInfo(AtgDev.Voicemeeter.RemoteApiExtender vmrApi)
        {
            void AddChannel(List<VmIOInfo> ioInfo, VmIOType type, int num)
            {
                VmIOInfo channel = new VmIOInfo
                {
                    Id = $"{type}({num})",
                    Type = type,
                    IsPhysical = num < (type == VmIOType.Strip ? MaxPhysicalStrips : MaxPhysicalBuses),
                };
                _ = vmrApi.GetParameter($"{channel.Id}.Label", out string label);
                channel.Name = label;
                ioInfo.Add(channel);
            }

            if (IOInfo is null)
            {
                List<VmIOInfo> ioInfo = new List<VmIOInfo>();
                for (int i = 0; i < MaxStrips; i++)
                {
                    AddChannel(ioInfo, VmIOType.Strip, i);
                }

                for (int i = 0; i < MaxBuses; i++)
                {
                    AddChannel(ioInfo, VmIOType.Bus, i);
                }


                ioInfo.Add(new VmIOInfo
                {
                    Id = nameof(VmIOType.Recorder),
                    Type = VmIOType.Recorder,
                    IsPhysical = false,
                });
                IOInfo = ioInfo;
            }
        }

        public static List<VmIOOptions> IOOptions { get; private set; }

        internal static void InitIOOptions()
        {
            if (IOOptions is null)
            {
                int maxPhysical = MaxPhysicalBuses;
                List<VmIOOptions> ioOptions = new List<VmIOOptions>();
                foreach (var channel in IOInfo)
                {
                    string channelId = channel.Id;
                    if (channel.Type != VmIOType.Recorder)
                    {
                        ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Mute", Type = VariableType.Bool });
                        ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Mono", Type = VariableType.Bool });
                        ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
                        ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Label", Type = VariableType.String });
                        if (channel.Type == VmIOType.Strip)
                        {
                            if (channel.IsPhysical)
                            {
                                ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Solo", Type = VariableType.Bool });
                            }

                            for (int i = 1; i <= MaxBuses; i++)
                            {
                                string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                                ioOptions.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                            }
                        }
                        else
                        {
                            if (IsBananaOrPotato)
                            {
                                ioOptions.Add(new VmIOOptions { Id = channelId, Option = "EQ.on", Type = VariableType.Bool });

                                if (ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64)
                                {
                                    ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Sel", Type = VariableType.Bool });
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
                                ioOptions.Add(new VmIOOptions { Id = channelId, Option = busOut, Type = VariableType.Bool });
                            }
                            ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Stop", Type = VariableType.Bool });
                            ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Play", Type = VariableType.Bool });
                            ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Record", Type = VariableType.Bool });
                            ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Pause", Type = VariableType.Bool });
                            ioOptions.Add(new VmIOOptions { Id = channelId, Option = "Gain", Type = VariableType.Float });
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
                        if (!ioOptions.Contains(addedVariable))
                        {
                            ioOptions.Add(addedVariable);
                        }
                    }
                }
                IOOptions = ioOptions;
            }
        }

        private static bool IsBananaOrPotato => ConnectedType == VoicemeeterType.Banana || ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64;

        public static List<VmIOCommand> IOCommands { get; private set; }

        internal static void InitIOCommands()
        {
            if (IOCommands is null)
            {
                List<VmIOCommand> availableCommands = new List<VmIOCommand>();
                var commands = Enum.GetValues(typeof(Commands));
                foreach (Commands command in commands)
                {
                    availableCommands.Add(new VmIOCommand()
                    {
                        CommandType = command,
                        RequiresValue = command.Equals(Commands.ConfigLoad) || command.Equals(Commands.ConfigSave) || command.Equals(Commands.RecorderLoad),
                    });
                }
                IOCommands = availableCommands;
            }
        }
    }
}
