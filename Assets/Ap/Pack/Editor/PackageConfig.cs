using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Ap.Tools;
using UnityEngine;

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

        // 完整项目生成的路径
        public static string PlayerOutPath
        {
            get
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
                return path + "/Players/" + Utility.GetPlatformName() + "/" + Version;
            }
        }
        /// <summary>
        /// 上传的路径
        /// </summary>
        public static string UpLoadUrl
        {
            get
            {
                return "127.0.0.1:5000//" + Utility.GetPlatformName() + "/" + Version;
            }
        }


        public static string Version
        {
            get
            {
                return PlayerSettings.bundleVersion;
            }
        }

        /// <summary>
        /// TOLUA 路径
        /// </summary>
        public static string ToLuaPath
        {
            get
            {
                return Application.dataPath + "/ToLua/Lua";
            }
        }
        /// <summary>
        /// 本项目的Lua 路径 
        /// </summary>
        public static string LuaPath
        {
            get
            {
                return Application.dataPath + "/Scripts/Lua";
            }
        }
        
        /// <summary>
        /// LUA 打包路径
        /// </summary>
        public static string LuaOutPath
        {
            get
            {
                return Application.streamingAssetsPath + "/Scripts";
            }
        }

        public static string DataOutPath
        {
            get
            {
                return Application.streamingAssetsPath + "/Data";
            }
        }
    }
}
