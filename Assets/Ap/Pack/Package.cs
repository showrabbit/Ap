using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Pack
{

    public class Package
    {
        // 1.打包路径
        // 打包assetbundle

        [MenuItem("Assets/AssetBundles/Build AssetBundles")]
        public static void Build()
        {
            IPackageBuilder builder = null;
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                builder = (new PackageAndroid()) as IPackageBuilder;
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                builder = (new PackageIOS()) as IPackageBuilder;
            }

            builder.Build(PackageConfig.OutputPath);

        }

    }
}
