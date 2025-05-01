using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyCoopInstaller
{
    internal class AutoSetup
    {
        public static bool Installing = true;
        public static void Begin()
        {
            GithubManager.PrepareReleasesList();

            GithubManager.AvalibleRelease R = GithubManager.GetLatestReleaseMeta(GithubManager.GameVersions[0], false);
            Downloader.SilentMode = true;

            string Path = AppDomain.CurrentDomain.BaseDirectory + "/DsMods";
            DeleteDirectory(Path);
            Directory.CreateDirectory(Path);
            Console.WriteLine($"Download to {Path}");
            Downloader.Start(R, Path, R.m_RequiredMelon);
        }

        public static void DeleteDirectory(string path, bool retryOnFailure = true, int maxRetries = 3, int retryDelay = 500)
        {
            if (!Directory.Exists(path))
                return;

            SetAttributesNormal(path);

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Directory.Delete(path, true);
                    return;
                }
                catch (IOException ex) when (retryOnFailure && attempt < maxRetries)
                {
                    Console.WriteLine($"Attempt {attempt}: {ex.Message}");
                    Thread.Sleep(retryDelay);
                }
                catch (UnauthorizedAccessException ex) when (retryOnFailure && attempt < maxRetries)
                {
                    Console.WriteLine($"Attempt {attempt}: {ex.Message}");
                    Thread.Sleep(retryDelay);
                }
            }

            throw new IOException($"Couldn't delete the '{path}' directory after {maxRetries} attempts");
        }

        private static void SetAttributesNormal(string path)
        {
            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                File.SetAttributes(file, FileAttributes.Normal);
            }

            foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                File.SetAttributes(dir, FileAttributes.Normal);
            }

            File.SetAttributes(path, FileAttributes.Normal);
        }
    }
}
