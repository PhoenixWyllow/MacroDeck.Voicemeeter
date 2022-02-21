using PW.MacroDeck.VoicemeeterPlugin.Models;

namespace PW.MacroDeck.VoicemeeterPlugin.ViewModels
{
    public interface ISoundboardBaseConfigViewModel
    {
        protected ISerializableConfiguration SerializableConfiguration { get; }

        void SetConfig();

        void SaveConfig();
    }
}
