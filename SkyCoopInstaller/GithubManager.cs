using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader.LightJson;

namespace SkyCoopInstaller
{
    internal class GithubManager
    {
        public const string MetadataURL = "https://api.github.com/repos/Filigrani/SkyCoop/releases";
        public static string GitJson = "";
        public static List<ReleaseMeta> Releaes = new List<ReleaseMeta>();
        public static List<AvalibleRelease> AvailableReleaes = new List<AvalibleRelease>();
        public static List<string> FilteredReleases = new List<string>();

        public class ReleaseMeta
        {
            public string m_ReleaseName = "";
            public string m_Tag = "";
            public string m_DownloadURL = "";
            public string m_ChangeLog = "";
        }
        public class DependenceMeta
        {
            public string m_Name = "";
            public string m_DownloadURL = "";
            public string m_Path = @"\Mods";
        }
        public class AvalibleRelease
        {
            public ReleaseMeta m_ReleaseMeta = new ReleaseMeta();
            public List<DependenceMeta> m_Dependencies = new List<DependenceMeta>();
        }

        public static string RequestGithubData()
        {
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            try
            {
                return Program.webClient.DownloadString(MetadataURL);
            }
            catch
            {
                return "";
            }
        }

        public static void BuildReleasesList()
        {

        }

        public static void PrepareReleasesList() 
        {
            GitJson = RequestGithubData();
            if (string.IsNullOrEmpty(GitJson))
            {
                return;
            }
            JsonArray JsonData = JsonValue.Parse(GitJson).AsJsonArray;
            foreach (JsonValue release in JsonData)
            {
                ReleaseMeta Meta = new ReleaseMeta();
                if (release["prerelease"].AsBoolean)
                {
                    continue;
                }
                JsonArray assets = release["assets"].AsJsonArray;
                if (assets.Count <= 0)
                {
                    continue;
                }
                Meta.m_ReleaseName = release["name"].AsString;
                Meta.m_Tag = release["tag_name"].AsString;
                Meta.m_DownloadURL = assets[0]["browser_download_url"].AsString;
                Meta.m_ChangeLog = release["body"].AsString;
            }
        }
    }
}
