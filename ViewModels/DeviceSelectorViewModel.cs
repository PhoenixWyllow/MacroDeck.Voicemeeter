using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using PW.VoicemeeterPlugin.Actions;

namespace PW.VoicemeeterPlugin.ViewModels;

public abstract class DeviceSelectorViewModel : ISavableConfigViewModel
{
    private readonly PluginAction _action;

    private readonly DeviceConfigModel _configuration;

    ISerializableConfiguration ISavableConfigViewModel.SerializableConfiguration => _configuration;

    protected DeviceSelectorViewModel(PluginAction action)
    {
        _action = action;
        _configuration = DeviceConfigModel.Deserialize(action.Configuration);
        if (_configuration.Option != null)
        {
            SelectedDevice = AvailableDevices.FirstOrDefault(d => d.Id.Equals(_configuration.Option.Id));
            AvailableActions = GetAvailableActionsForDevice(SelectedDevice);
            ChangeAction(_configuration.Action);
        }
    }

    public string[] AvailableActions { get; private set; }
    public IEnumerable<VmIoInfo> AvailableDevices { get; } = AvailableValues.IoInfo;
    public VmIoInfo SelectedDevice { get; private set; }
    public string SelectedAction { get; private set; }
    public bool IsSlider => _action is DeviceSliderAction;

    public float SliderValue
    {
        get => _configuration.Value; 
        set => _configuration.Value = value;
    }

    public void ChangeDevice(VmIoInfo selectedDevice)
    {
        SelectedDevice = AvailableDevices.FirstOrDefault(device => device.Equals(selectedDevice));
        AvailableActions = GetAvailableActionsForDevice(SelectedDevice);
    }

    protected abstract string[] GetAvailableActionsForDevice(VmIoInfo device);

    public void ChangeAction(string selectedAction)
    {
        SelectedAction = selectedAction;
    }

    public void SaveConfig()
    {
        try
        {
            SetConfig();
            MacroDeckLogger.Info(PluginInstance.Plugin, $"{GetType().Name}: config saved");
        }
        catch (Exception ex)
        {
            MacroDeckLogger.Error(PluginInstance.Plugin, $"{GetType().Name}: config NOT saved");
            MacroDeckLogger.Error(PluginInstance.Plugin, $"{GetType().Name}: {ex.Message}");
        }
    }

    public void SetConfig()
    {
        _configuration.Name = SelectedDevice.ToString();
        _configuration.Action = SelectedAction;
        _configuration.Option = AvailableValues.IoOptions.Find(option => option.Option.Equals(SelectedAction) && option.Id.Equals(SelectedDevice.Id));

        _action.ConfigurationSummary = _configuration.ToString();
        _action.Configuration = _configuration.Serialize();
        if (!IsSlider)
        {
            _action.BindableVariable = _configuration.Option.AsVariable;
        }
    }
}