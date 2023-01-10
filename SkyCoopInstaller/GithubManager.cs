using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    internal class GithubManager
    {
        public const string MetadataURL = "https://api.github.com/repos/Filigrani/SkyCoop/releases";
        public static string GitJson = "";

        public static string RequestGithubData()
        {
            try
            {
                return Program.webClient.DownloadString(MetadataURL);
            }
            catch
            {
                return "";
            }
        }

        public static void PrepareReleasesList() 
        {
            GitJson = RequestGithubData();
            if (string.IsNullOrEmpty(GitJson))
            {
                return;
            }

        }
    }
}
