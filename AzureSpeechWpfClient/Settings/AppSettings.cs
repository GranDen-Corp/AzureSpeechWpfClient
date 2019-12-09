using Jot.Configuration.Attributes;

namespace AzureSpeechWpfClient.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {
            this.DisplaySettings = new DisplaySettings();
            this.RuntimeSettings = new RuntimeSettings();
        }


        [Trackable]
        public DisplaySettings DisplaySettings { get; set; }

        [Trackable]
        public RuntimeSettings RuntimeSettings { get; set; }

    }
}
