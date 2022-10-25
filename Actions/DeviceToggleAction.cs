using System.Linq;
using PW.VoicemeeterPlugin.Models;
using PW.VoicemeeterPlugin.Services;
using PW.VoicemeeterPlugin.Services.Voicemeeter;
using PW.VoicemeeterPlugin.ViewModels;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;

namespace PW.VoicemeeterPlugin.Actions
{
    public sealed class DeviceToggleAction : PluginAction
    {
        /// <summary>
        /// Name of the action
        /// </summary>
        public override string Name => LocalizationManager.Instance.ToggleActionName;

        /// <summary>
        /// A short description what this action does
        /// </summary>
        public override string Description => LocalizationManager.Instance.ToggleActionDescription;

        /// <summary>
        /// Set true if the plugin can be configured.
        /// </summary>
        public override bool CanConfigure => true;

        /// <summary>
        /// Return the ActionConfigControl for this action.
        /// </summary>
        /// <returns></returns>
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new Views.DeviceSelectorConfigView(new DeviceToggleViewModel(this));
        }

        /// <summary>
        /// Gets called when the button with this action gets pressed.
        /// </summary>
        /// <param name="clientId">Returns the client id</param>
        /// <param name="actionButton">Returns the pressed action button</param>
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (string.IsNullOrWhiteSpace(Configuration))
            {
                return;
            }
            var config = DeviceConfigModel.Deserialize(Configuration);
            var value = VariableManager.GetVariables(PluginInstance.Plugin).FirstOrDefault(v => v.Name.Equals(config.Option.AsVariable));
            PluginInstance.VoicemeeterControl.SetParameter(config.Option.AsParameter, value.Value.Equals(bool.FalseString) ? Constants.On : Constants.Off);
        }
    }
}
