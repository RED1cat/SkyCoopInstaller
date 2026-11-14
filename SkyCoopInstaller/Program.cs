using Avalonia;
using System;
using System.Net;

namespace SkyCoopInstaller;

class Program
{
    public static WebClient webClient = new WebClient();
    public static MainWindow mainWindow;
    [STAThread]
    public static void Main(string[] args) 
    {
        webClient = new WebClient();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
