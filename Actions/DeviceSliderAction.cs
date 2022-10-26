using System.Linq;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;

namespace PW.VoicemeeterPlugin.Actions;

public sealed class DeviceSliderAction : PluginAction
{
    public override string Name => LocalizationManager.Instance.SliderActionName;
    public override string Description => LocalizationManager.Instance.SliderActionDescription;
    public override bool CanConfigure => true;
    
    public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
    {
        return new Views.DeviceSelectorConfigView(new DeviceSliderViewModel(this));
    }
    
    public override void Trigger(string clientId, ActionButton actionButton)
    {
        if (string.IsNullOrWhiteSpace(Configuration))
        {
            return;
        }
        var config = DeviceConfigModel.Deserialize(Configuration);
        var value = VariableManager.GetVariables(PluginInstance.Plugin).FirstOrDefault(v => v.Name.Equals(config.Option.AsVariable));
        if (float.TryParse(value.Value, out float valueF))
        {
            PluginInstance.VoicemeeterControl.SetParameter(config.Option.AsParameter, valueF + config.Value);
        }
    }
}