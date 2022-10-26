using AtgDev.Voicemeeter.Types;
using PW.VoicemeeterPlugin.Models;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;

namespace PW.VoicemeeterPlugin.Services.Voicemeeter;

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
        IoInfo = null;
        IoOptions = null;
        IoCommands = null;
    }

    public static List<VmIoInfo> IoInfo { get; private set; }

    internal static void InitIoInfo(AtgDev.Voicemeeter.RemoteApiExtender vmrApi)
    {
        void AddChannel(List<VmIoInfo> ioInfo, VmIoType type, int num)
        {
            var channelId = $"{type}({num})";
            _ = vmrApi.GetParameter($"{channelId}.Label", out string label);
            VmIoInfo channel = new()
            {
                Id = channelId,
                Name = label,
                Type = type,
                IsPhysical = num < (type == VmIoType.Strip ? MaxPhysicalStrips : MaxPhysicalBuses),
            };
            ioInfo.Add(channel);
        }

        if (IoInfo is null)
        {
            List<VmIoInfo> ioInfo = new();
            for (int i = 0; i < MaxStrips; i++)
            {
                AddChannel(ioInfo, VmIoType.Strip, i);
            }

            for (int i = 0; i < MaxBuses; i++)
            {
                AddChannel(ioInfo, VmIoType.Bus, i);
            }


            ioInfo.Add(new()
            {
                Id = nameof(VmIoType.Recorder),
                Type = VmIoType.Recorder,
                IsPhysical = false,
            });
            IoInfo = ioInfo;
        }
    }

    public static List<VmIoOptions> IoOptions { get; private set; }

    internal static void InitIoOptions()
    {
        if (IoOptions is null)
        {
            int maxPhysical = MaxPhysicalBuses;
            List<VmIoOptions> ioOptions = new();
            foreach (var channel in IoInfo)
            {
                string channelId = channel.Id;
                if (channel.Type != VmIoType.Recorder)
                {
                    ioOptions.Add(new() { Id = channelId, Option = "Mute", Type = VariableType.Bool });
                    ioOptions.Add(new() { Id = channelId, Option = "Mono", Type = VariableType.Bool });
                    ioOptions.Add(new() { Id = channelId, Option = "Gain", Type = VariableType.Float });
                    ioOptions.Add(new() { Id = channelId, Option = "Label", Type = VariableType.String });
                    if (channel.Type == VmIoType.Strip)
                    {
                        if (channel.IsPhysical)
                        {
                            ioOptions.Add(new() { Id = channelId, Option = "Solo", Type = VariableType.Bool });
                        }

                        for (int i = 1; i <= MaxBuses; i++)
                        {
                            string busOut = i <= maxPhysical ? $"A{i}" : $"B{i - maxPhysical}";
                            ioOptions.Add(new() { Id = channelId, Option = busOut, Type = VariableType.Bool });
                        }
                    }
                    else
                    {
                        if (IsBananaOrPotato)
                        {
                            ioOptions.Add(new() { Id = channelId, Option = "EQ.on", Type = VariableType.Bool });

                            if (ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64)
                            {
                                ioOptions.Add(new() { Id = channelId, Option = "Sel", Type = VariableType.Bool });
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
                            ioOptions.Add(new() { Id = channelId, Option = busOut, Type = VariableType.Bool });
                        }
                        ioOptions.Add(new() { Id = channelId, Option = "Stop", Type = VariableType.Bool });
                        ioOptions.Add(new() { Id = channelId, Option = "Play", Type = VariableType.Bool });
                        ioOptions.Add(new() { Id = channelId, Option = "Record", Type = VariableType.Bool });
                        ioOptions.Add(new() { Id = channelId, Option = "Pause", Type = VariableType.Bool });
                        ioOptions.Add(new() { Id = channelId, Option = "Gain", Type = VariableType.Float });
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
            IoOptions = ioOptions;
        }
    }

    private static bool IsBananaOrPotato => ConnectedType == VoicemeeterType.Banana || ConnectedType == VoicemeeterType.Potato || ConnectedType == VoicemeeterType.Potato64;

    public static List<VmIoCommand> IoCommands { get; private set; }

    internal static void InitIoCommands()
    {
        if (IoCommands is null)
        {
            List<VmIoCommand> availableCommands = new();
            var commands = Enum.GetValues(typeof(Commands));
            foreach (Commands command in commands)
            {
                availableCommands.Add(new()
                {
                    CommandType = command,
                    RequiresValue = command.Equals(Commands.ConfigLoad) || command.Equals(Commands.ConfigSave) || command.Equals(Commands.RecorderLoad),
                });
            }
            IoCommands = availableCommands;
        }
    }
}