using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    internal class MelonChecker
    {
        public static Version GetMelonVersion(string dirpath)
        {
            string folder_path = Path.Combine(dirpath, "MelonLoader");
            string legacy_file_path = Path.Combine(folder_path, "MelonLoader.ModHandler.dll");
            string file_path = Path.Combine(folder_path, "MelonLoader.dll");
            if (!File.Exists(legacy_file_path) && !File.Exists(file_path))
            {
                return null;
            }

            string fileversion = null;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo((File.Exists(legacy_file_path) ? legacy_file_path : file_path));
            if (fileVersionInfo != null)
            {
                fileversion = fileVersionInfo.ProductVersion;
                if (string.IsNullOrEmpty(fileversion))
                {
                    fileversion = fileVersionInfo.FileVersion;
                }
            }
            if (string.IsNullOrEmpty(fileversion))
            {
                fileversion = "0.0.0.0";
            }
                
            return new Version(fileversion);
        }

        public static bool CheckInstalledMelonVersion(string GameDir, string RequiredVersion)
        {
            Version CurrentVersion = GetMelonVersion(GameDir);
            if (CurrentVersion == null)
            {
                return false;
            }else
            {
                return CurrentVersion.ToString() == RequiredVersion;
            }
        }
    }
}
