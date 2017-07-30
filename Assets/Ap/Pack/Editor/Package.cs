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
        // 打包路径
        // 打包assetbundle
        // 重命名AssetBundleManifest 为平台的名称
        // 移动assetbundle到StreamingAssets 路径下
        // 生成完整的包
        // 上传完整包和assetbundles
        [MenuItem("Assets/AssetBundles/Build")]
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
            builder.BuildLua(PackageConfig.LuaOutPath);
            builder.BuildAssetBundle(PackageConfig.OutputPath);
            builder.BuildPlayer(PackageConfig.PlayerOutPath);
            builder.UpLoad(PackageConfig.UpLoadUrl);
            builder.Clear();
        }

    }
}
