using Jot.Configuration;
using Jot.Configuration.Attributes;

namespace AzureSpeechWpfClient.Settings
{
    public class AppSettings : ITrackingAware<AppSettings>
    {
        public AppSettings()
        {
            DisplaySettings = new DisplaySettings();
            RuntimeSettings = new RuntimeSettings();
        }

        public DisplaySettings DisplaySettings { get; set; }

        public RuntimeSettings RuntimeSettings { get; set; }

        public void ConfigureTracking(TrackingConfiguration<AppSettings> configuration)
        {
            configuration.Properties(s => new { s.DisplaySettings, s.RuntimeSettings });
            System.Windows.Application.Current.Exit += (s, e) => { configuration.Tracker.Persist(this); };
        }
    }
}
