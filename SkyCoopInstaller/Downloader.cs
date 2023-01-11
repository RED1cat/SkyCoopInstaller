using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    internal class Downloader
    {
        private static GithubManager.AvalibleRelease selectedToDownload;

        public static void Start(GithubManager.AvalibleRelease release, string gamePath, string melonVersion)
        {
            selectedToDownload = release;

            if(MelonChecker.CheckInstalledMelonVersion(gamePath, melonVersion) == false)
            {

            }

            foreach (GithubManager.DependenceMeta dependenceMeta in release.m_Dependencies)
            {
                DownloadFromUrl(dependenceMeta.m_DownloadURL, gamePath, dependenceMeta.m_Name, dependenceMeta.m_IsZip);
            }
        }
        private static void DownloadFromUrl(string url, string path, string name,  bool isZip) 
        {
            Program.webClient.Headers.Clear();
            Program.webClient.Headers.Add("Cache-Control", "no-cache");
            Program.webClient.Headers.Add("User-Agent", "Unity web player");
            Program.webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            
            Program.webClient.DownloadFile(new Uri(url), path);
            if (isZip)
            {
                ZipFile.ExtractToDirectory(path + @"\" + name + ".zip", path);
                File.Delete(path + @"\" + name + ".zip");
            }
        }
    }
}
