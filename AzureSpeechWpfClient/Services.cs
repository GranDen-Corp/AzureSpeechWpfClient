using System.Windows;
using Jot;

namespace AzureSpeechWpfClient
{
    static class Services
    {
        public static Tracker Tracker = new Tracker();

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
