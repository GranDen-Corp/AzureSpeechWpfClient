using System;
using System.Reflection;
using System.Windows;
using Jot;
using Jot.Storage;

namespace AzureSpeechWpfClient
{
    static class Services
    {
        public static Tracker Tracker = CreateTracker();

        private static Tracker CreateTracker()
        {
            var currentPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var configFilePath = System.IO.Path.Combine(currentPath, "SpeechSDKConfig");

            return new Tracker(new JsonFileStore(configFilePath));
        }

        static Services()
        {
            Tracker.Configure<Window>()
                .Id(w => w.Title)
                .Properties(w => new {w.Top, w.Width, w.Height, w.Left, w.WindowState})
                .PersistOn(nameof(Window.Closing))
                .StopTrackingOn(nameof(Window.Closing));
        }
    }
}
