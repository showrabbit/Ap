using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Ap.Tools;

namespace Ap.Pack
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageConfig
    {
        //
        public static string OutputPath
        {
            get
            {
                return "AssetBundles/" + Utility.GetPlatformName() + "/" + Version;
            }
        }

        public static string Version
        {
            get
            {
                return PlayerSettings.bundleVersion;
            }
        }
    }
}
