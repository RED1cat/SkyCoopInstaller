using Avalonia;
using System;
using System.Net;

namespace SkyCoopInstaller;

class Program
{
    public static WebClient webClient = new WebClient();
    [STAThread]
    public static void Main(string[] args) 
    { 
        if (args.GetLength(0) == 0)
        {
            webClient = new WebClient();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        else if (args[0] == "-silent")
        {
            Console.WriteLine("Silent Mode");
            Console.ReadKey();
        }
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
