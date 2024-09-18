using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    internal class AutoSetup
    {
        public static bool IsRequired()
        {
            return Environment.GetCommandLineArgs().Contains("-auto");
        }
        public static void Begin()
        {
            GithubManager.PrepareReleasesList();

            GithubManager.AvalibleRelease R = GithubManager.GetLatestReleaseMeta(GithubManager.GameVersions[0], false);
            Downloader.SilentMode = true;

            string Path = Environment.CurrentDirectory + "/DsMods";
            if(Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
            Directory.CreateDirectory(Path);
            Downloader.Start(R, Path, "");
        }

        public static void Finished()
        {
            string Path = Environment.CurrentDirectory + "/DsMods";
            if (Directory.Exists(Path))
            {
                if(File.Exists(Path + "/Mods/Steamworks.NET.dll"))
                {
                    File.Delete(Path + "/Mods/Steamworks.NET.dll");
                }
                if (File.Exists(Path + "/Mods/multiplayerstuff.unity3d"))
                {
                    File.Delete(Path + "/Mods/multiplayerstuff.unity3d");
                }
            }
            Application.Exit();
        }
    }
}
