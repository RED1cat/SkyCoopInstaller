using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MelonLoader.LightJson;

namespace SkyCoopInstaller
{
    internal class GithubManager
    {
        public const string MetadataURL = "https://api.github.com/repos/Filigrani/SkyCoop/releases";
        public const string FilteredURL = "https://raw.githubusercontent.com/RED1cat/SkyCoopInstaller/master/FilteredReleases.json";
        public const string NewsURL = "https://raw.githubusercontent.com/RED1cat/SkyCoopInstaller/master/News.txt";
        public static string GitJson = "";
        public static string FilteredJson = "";
        public static List<ReleaseMeta> Releaes = new List<ReleaseMeta>();
        public static List<string> GameVersions = new List<string>();
        public static Dictionary<string, List<AvalibleRelease>> AvailableReleaes = new Dictionary<string, List<AvalibleRelease>>();
        public static string News = "";

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
            public string m_Path = "";
            public bool m_IsZip = false;
        }
        public class AvalibleRelease
        {
            public ReleaseMeta m_ReleaseMeta = new ReleaseMeta();
            public List<DependenceMeta> m_Dependencies = new List<DependenceMeta>();
            public bool m_IsPreRelease = false;
        }

        public static string RequestRemoteFile(string URL)
        {
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("Cache-Control", "no-cache");
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
            try
            {
                return Program.webClient.DownloadString(URL);
            }
            catch
            {
                return "";
            }
        }

        public static void PrepareReleasesList() 
        {
            GitJson = RequestRemoteFile(MetadataURL);
            if (string.IsNullOrEmpty(GitJson))
            {
                return;
            }
            FilteredJson = RequestRemoteFile(FilteredURL);
            if (string.IsNullOrEmpty(FilteredJson))
            {
                return;
            }
            News = RequestRemoteFile(NewsURL);
            JsonArray JsonData = JsonValue.Parse(GitJson).AsJsonArray;
            foreach (JsonValue release in JsonData)
            {
                ReleaseMeta Meta = new ReleaseMeta();
                JsonArray assets = release["assets"].AsJsonArray;
                if (assets.Count <= 0)
                {
                    continue;
                }
                Meta.m_ReleaseName = release["name"].AsString;
                Meta.m_Tag = release["tag_name"].AsString;
                Meta.m_DownloadURL = assets[0]["browser_download_url"].AsString;
                Meta.m_ChangeLog = release["body"].AsString;
                Releaes.Add(Meta);
            }
            JsonArray FilteredJsonData = JsonValue.Parse(FilteredJson).AsJsonArray;
            foreach (JsonValue gameversion in FilteredJsonData)
            {
                string g_version = gameversion["game_version"].AsString;
                GameVersions.Add(g_version);
                List<AvalibleRelease> ReleasesForGameVersion = new List<AvalibleRelease>();
                foreach (JsonValue modversion in gameversion["mod_versions"].AsJsonArray)
                {
                    AvalibleRelease AvRelease = new AvalibleRelease();
                    foreach (ReleaseMeta Meta in Releaes)
                    {
                        if(Meta.m_Tag == modversion["tag_name"].AsString)
                        {
                            AvRelease.m_ReleaseMeta = Meta;
                            break;
                        }
                    }
                    foreach (JsonValue Dependence in modversion["dependencies"].AsJsonArray)
                    {
                        DependenceMeta DepMeta = new DependenceMeta();
                        DepMeta.m_Name = Dependence["name"].AsString;
                        DepMeta.m_DownloadURL = Dependence["url"].AsString;
                        DepMeta.m_Path = @"\"+ Dependence["path"].AsString;

                        string[] Slices = DepMeta.m_DownloadURL.Split('.');
                        if(Slices.Length > 1)
                        {
                            if(Slices[Slices.Length-1].ToLower() == "zip")
                            {
                                DepMeta.m_IsZip = true;
                            }
                        }

                        AvRelease.m_Dependencies.Add(DepMeta);
                    }
                    ReleasesForGameVersion.Add(AvRelease);
                }
                AvailableReleaes.Add(g_version, ReleasesForGameVersion);
            }
        }

        public static List<AvalibleRelease> GetReleasesForGameVersion(string game_version, bool HidePreReleases = true)
        {
            List<AvalibleRelease> AllReleases;
            if(AvailableReleaes.TryGetValue(game_version, out AllReleases))
            {
                if (!HidePreReleases)
                {
                    return AllReleases;
                } else
                {
                    List<AvalibleRelease> OnlyStable = new List<AvalibleRelease>();
                    foreach (AvalibleRelease Release in AllReleases)
                    {
                        if (!Release.m_IsPreRelease)
                        {
                            OnlyStable.Add(Release);
                        }
                    }
                    return OnlyStable;
                }
            }
            return null;
        }
        public static List<string> GetGameVersions()
        {
            return GameVersions;
        }
        public static string GetNews()
        {
            return News;
        }
    }
}
