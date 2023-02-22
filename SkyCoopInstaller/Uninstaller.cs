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
        private static List<UninstallerQueueElement> UninstallerQueue = new List<UninstallerQueueElement>();
        private static int ProgressValue = 0;
        public class UninstallerQueueElement
        {
            public string m_Path = "";
            public string m_FileName = "";
            public bool m_IsFile;

            public UninstallerQueueElement(string path, string filename, bool isFile = true)
            {
                m_Path = path;
                m_FileName = filename;
                m_IsFile = isFile;
            }
        }
        public static void Start(GithubManager.AvalibleRelease release, string gamePath, bool fullUninstall)
        {
            
            if(fullUninstall == false)
            {
                foreach (GithubManager.DependenceMeta dependence in release.m_Dependencies)
                {
                    UninstallerQueue.Add(new UninstallerQueueElement(gamePath + dependence.m_Path, dependence.m_Name + ".dll"));
                }
            }
            else
            {
                if(Directory.Exists(gamePath + @"\MelonLoader"))
                {
                    UninstallerQueue.Add(new UninstallerQueueElement(gamePath, "MelonLoader", false));
                }
                if(File.Exists(gamePath + @"\version.dll"))
                {
                    UninstallerQueue.Add(new UninstallerQueueElement(gamePath, "version.dll"));
                }
                if(Directory.Exists(gamePath + @"\Mods"))
                {
                    UninstallerQueue.Add(new UninstallerQueueElement(gamePath, "Mods", false));
                }
                if (Directory.Exists(gamePath + @"\Plugins"))
                {
                    UninstallerQueue.Add(new UninstallerQueueElement(gamePath, "Plugins", false));
                }
            }
            Program.mainForm.SetMaximumValueProgressBar(UninstallerQueue.Count);
            BeginUninstall();
        }
        private static void BeginUninstall()
        {
            foreach(UninstallerQueueElement file in UninstallerQueue)
            {
                if (file.m_IsFile)
                {
                    try
                    {
                        File.Delete(file.m_Path + @"\" + file.m_FileName);
                    }
                    finally
                    {
                        ProgressValue++;
                        Program.mainForm.AddInstallLog($"File {file.m_FileName} Delete complete");
                        Program.mainForm.UpdateCurrentFileProgressBar(ProgressValue);
                    }
                }
                else
                {
                    try
                    {
                        Directory.Delete(file.m_Path + @"\" + file.m_FileName, true);
                    }
                    finally
                    {
                        ProgressValue++;
                        Program.mainForm.AddInstallLog($"File {file.m_FileName} Delete complete");
                        Program.mainForm.UpdateCurrentFileProgressBar(ProgressValue);
                    }
                }
            }
            UninstallerQueue.Clear();
            Program.mainForm.InstallFinished("Uninstallation");
        }
    }
}
