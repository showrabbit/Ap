using Ap.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Pack
{
    public class PackageTools
    {
        public static void CopyToStreamingAssets(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            // 重命名
            if (File.Exists(path + "/" + PackageConfig.Version))
            {
                if (File.Exists(path + "/" + Utility.GetPlatformName()))
                    File.Delete(path + "/" + Utility.GetPlatformName());
                File.Move(path + "/" + PackageConfig.Version, path + "/" + Utility.GetPlatformName());
            }
            // 拷贝
            string toPath = Application.streamingAssetsPath + "/AssetBundles";
            if (Directory.Exists(toPath))
            {
                Directory.Delete(toPath, true);
            }
            Directory.CreateDirectory(toPath);

            foreach (string f in Directory.GetFiles(path))
            {
                if (f.ToLower().Contains("manifest"))
                {
                    continue;
                }

                string file = f.Replace("\\", "/");
                file = file.Substring(file.LastIndexOf("/"));
                File.Copy(f, toPath + "/" + file);
            }
        }

        public static string[] GetLevelsFromBuildSettings()
        {
            List<string> levels = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                    levels.Add(EditorBuildSettings.scenes[i].path);
            }

            return levels.ToArray();
        }
    }
}
