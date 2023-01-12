using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssetsTools.NET;
using AssetsTools.NET.Extra;

namespace SkyCoopInstaller
{
    internal class GameVersionChecker
    {
        public static string GameVersion = "";
        public static string GameDeveloper = "";
        public static string GameName = "";
        private static void ReadGameInfo(AssetsManager assetsManager, string gameDataPath)
        {
            AssetsFileInstance instance = null;
            try
            {
                string bundlePath = Path.Combine(gameDataPath, "globalgamemanagers");
                if (!File.Exists(bundlePath))
                    bundlePath = Path.Combine(gameDataPath, "mainData");

                if (!File.Exists(bundlePath))
                {
                    bundlePath = Path.Combine(gameDataPath, "data.unity3d");
                    if (!File.Exists(bundlePath))
                        return;

                    BundleFileInstance bundleFile = assetsManager.LoadBundleFile(bundlePath);
                    instance = assetsManager.LoadAssetsFileFromBundle(bundleFile, "globalgamemanagers");
                } else
                    instance = assetsManager.LoadAssetsFile(bundlePath, true);
                if (instance == null)
                    return;

                //assetsManager.LoadClassPackage(Assembly.GetExecutingAssembly().GetManifestResourceStream("MelonLoader.Resources.classdata.tpk"));

                //if (!instance.file.Metadata.TypeTreeEnabled)
                //{
                //    string UnityVersion = instance.file.Metadata.UnityVersion;
                //    assetsManager.LoadClassDatabaseFromPackage(UnityVersion);
                //}

                List<AssetFileInfo> assetFiles = instance.file.GetAssetsOfType(AssetClassID.PlayerSettings);
                if (assetFiles.Count > 0)
                {
                    AssetFileInfo playerSettings = assetFiles.First();

                    AssetTypeValueField playerSettings_baseField = assetsManager.GetBaseField(instance, playerSettings);
                    if (playerSettings_baseField != null)
                    {
                        AssetTypeValueField bundleVersion = playerSettings_baseField.Get("bundleVersion");
                        if (bundleVersion != null)
                            GameVersion = bundleVersion.AsString;

                        AssetTypeValueField companyName = playerSettings_baseField.Get("companyName");
                        if (companyName != null)
                            GameDeveloper = companyName.AsString;

                        AssetTypeValueField productName = playerSettings_baseField.Get("productName");
                        if (productName != null)
                            GameName = productName.AsString;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (instance != null)
                instance.file.Close();
        }


        public static string GetGameVersion(string GamePath)
        {
            if(Directory.Exists(GamePath+ @"\tld_Data"))
            {
                AssetsManager assetsManager = new AssetsManager();
                //ReadGameInfo(assetsManager, GamePath + @"\tld_Data");
                assetsManager.UnloadAll();
            }
            return GameVersion;
        }
    }
}
