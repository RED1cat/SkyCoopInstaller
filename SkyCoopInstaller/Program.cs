using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    internal static class Program
    {
        public static WebClient webClient;
        public static MainForm mainForm;
        [STAThread]
        static void Main(string[] arg)
        {
            if (arg.GetLength(0) == 0) 
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                webClient = new WebClient();
                mainForm = new MainForm();
                Application.Run(mainForm);
            }
            else if (arg[0] == "-silent")
            {
                webClient = new WebClient();
                mainForm = new MainForm(true);
                AllocConsole();
                Console.WriteLine("Silent Mode");
                Task.Run(AutoSetup.Begin);
                Wating();
            }
        }

        private static void Wating()
        {
            while (AutoSetup.Installing) {}
            FreeConsole();
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
