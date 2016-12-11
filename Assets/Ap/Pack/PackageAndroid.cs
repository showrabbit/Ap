using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Ap.Pack
{
    /// <summary>
    /// 打包android
    /// </summary>
    public class PackageAndroid : IPackageBuilder
    {
        public void Build(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}
