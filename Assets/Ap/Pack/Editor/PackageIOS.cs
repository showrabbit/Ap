using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Pack
{
    /// <summary>
    /// 打包ios
    /// </summary>
    public class PackageIOS : IPackageBuilder
    {

        public void BuildLua(string path)
        {
            PackageTools.PackLua(path);
        }
        
        public void BuildAssetBundle(string path)
        {
            PackageTools.PackAssetBundle(path);
        }


        public void BuildPlayer(string path)
        {
            string[] levels = PackageTools.GetLevelsFromBuildSettings();
            if (levels.Length == 0)
            {
                Debug.Log("Nothing to build.");
                return;
            }
            string topath = path;
            if (Directory.Exists(topath))
            {
                Directory.Delete(topath, true);
            }
            Directory.CreateDirectory(topath);
            BuildOptions option = EditorUserBuildSettings.development ? BuildOptions.Development : BuildOptions.None;
            BuildPipeline.BuildPlayer(levels, topath, EditorUserBuildSettings.activeBuildTarget, option);
        }

        public void Clear()
        {
            AssetDatabase.Refresh();
        }

        public void UpLoad(string url)
        {

        }
    }
}
