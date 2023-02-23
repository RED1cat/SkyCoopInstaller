using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCoopInstaller
{
    internal class Uninstaller
    {
        private static List<string> m_UninstallerQueue = new List<string>();
        private static int m_ProgressValue = 0;

        public static void Start(GithubManager.AvalibleRelease release, string gamePath, bool fullUninstall)
        {
            m_ProgressValue = 0;
            if (fullUninstall)
            {
                m_UninstallerQueue.Add($"{gamePath}\\MelonLoader");
                m_UninstallerQueue.Add($"{gamePath}\\Mods");
                m_UninstallerQueue.Add($"{gamePath}\\Plugins");
                m_UninstallerQueue.Add($"{gamePath}\\version.dll");
                m_UninstallerQueue.Add($"{gamePath}\\NOTICE.txt");
            }
            else
            {
                foreach (string path in release.m_UninstallContent)
                {
                    m_UninstallerQueue.Add($"{gamePath}\\{path}");
                }
                foreach(GithubManager.DependenceMeta dependence in release.m_Dependencies)
                {
                    m_UninstallerQueue.Add($"{gamePath}{dependence.m_Path}\\{dependence.m_Name}.dll");
                }
            }
            Program.mainForm.SetMaximumValueProgressBar(m_UninstallerQueue.Count);
            BeginUninstall();
        }
        private static void BeginUninstall()
        {
            foreach(string file in m_UninstallerQueue)
            {
                if (Directory.Exists(file))
                {
                    string name = new DirectoryInfo(file).Name;

                    Program.mainForm.AddInstallLog($"Trying delete file {name}");
                    try
                    {
                        Directory.Delete(file, true);
                    }
                    finally
                    {
                        m_ProgressValue++;
                        Program.mainForm.AddInstallLog($"File {file} Delete complete");
                        Program.mainForm.UpdateCurrentFileProgressBar(m_ProgressValue, name);
                        Program.mainForm.UpdateTotalProgressBar(m_ProgressValue);
                    }
                }
                else if(File.Exists(file))
                {
                    string name = Path.GetFileName(file);

                    Program.mainForm.AddInstallLog($"Trying delete file {name}");
                    try
                    {
                        File.Delete(file);
                    }
                    finally
                    {
                        m_ProgressValue++;
                        Program.mainForm.AddInstallLog($"File {file} Delete complete");
                        Program.mainForm.UpdateCurrentFileProgressBar(m_ProgressValue, name);
                        Program.mainForm.UpdateTotalProgressBar(m_ProgressValue);
                    }
                }
            }
            m_UninstallerQueue.Clear();
            Program.mainForm.InstallFinished("Uninstallation");
        }
    }
}
