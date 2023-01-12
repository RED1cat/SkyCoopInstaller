using System;
using System.Net;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    internal static class Program
    {
        public static WebClient webClient;
        public static MainForm mainForm;
        [STAThread]
        static void Main()
        {
            webClient = new WebClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
