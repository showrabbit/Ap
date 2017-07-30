using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Base
{
    /// <summary>
    /// 系统环境
    /// </summary>
    public class Environment
    {
        public const string AssetBundlesOutputPath = "AssetBundles";

        public static string GetPlatformName()
        {
#if UNITY_EDITOR
            return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
			return GetPlatformForAssetBundles(Application.platform);
#endif
        }

#if UNITY_EDITOR
        private static string GetPlatformForAssetBundles(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.WebPlayer:
                    return "WebPlayer";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
                    return "OSX";
                // Add more build targets for your own.
                // If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
                default:
                    return null;
            }
        }
#endif

        private static string GetPlatformForAssetBundles(RuntimePlatform platform)
        {
            switch (platform)
            {
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.OSXWebPlayer:
                case RuntimePlatform.WindowsWebPlayer:
                    return "WebPlayer";
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                    return "OSX";
                // Add more build targets for your own.
                // If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
                default:
                    return null;
            }
        }

        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                if (Application.isEditor)
                {
                    return Application.dataPath + "/Resources/Data/";
                }
                else
                    return Application.persistentDataPath + "/Data/";
            }
        }

        /// <summary>
        /// 存档路径
        /// </summary>
        public static string SavePath
        {
            get
            {
                return Application.persistentDataPath;
            }
        }

        /// <summary>
        /// AssetBundle 更新的路径
        /// </summary>
        public static string AssetBundleUpdatePath
        {
            get
            {
                if (Application.isEditor)
                {
                    return Application.dataPath + "/";
                }
                else
                {
                    return Application.persistentDataPath + "/AssetBundles/";
                }
            }
        }

        /// <summary>
        /// AssetBundle 发布后的路径
        /// </summary>
        public static string AssetBundlePath
        {
            get
            {
                if (Application.isEditor)
                {
                    return Application.dataPath + "/";
                }
                else
                {
                    return Application.streamingAssetsPath + "/AssetBundles/";
                }
            }
        }


        /// <summary>
        /// 本地数据库配置路径
        /// </summary>
        public static string DbPath
        {
            get
            {
                return "Data Source=" + Environment.DataPath + "/db.txt"; ;
            }
        }
        /// <summary>
        /// 本地用户数据路径
        /// </summary>
        public static string DbUserPath
        {
            get
            {
                return "Data Source=" + Environment.DataPath + "/user.txt"; ;
            }
        }
        /// <summary>
        /// lua 脚本路径
        /// </summary>
        public static string LuaPath
        {
            get
            {
#if UNITY_EDITOR
                return Application.dataPath + "/Scripts/Lua/";
#else
                return Application.persistentDataPath + "/Scripts/Lua/";
#endif
            }
        }

        public static string FrameworkPath
        {
            get
            {
                return Application.dataPath + "/Ap/";
            }
        }

        /// <summary>
        /// 游戏运行中的临时文件夹
        /// </summary>
        public static string TempPath
        {
            get
            {
#if UNITY_EDITOR
                return Application.dataPath + "/Temp/";
#else
                return Application.persistentDataPath + "/Temp/";
#endif
            }
        }

        /// <summary>
        /// 游戏运行中的日志文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
#if UNITY_EDITOR
                return Application.dataPath + "/Log/";
#else
                return Application.persistentDataPath + "/Log/";
#endif
            }
        }


        public static RuntimePlatform Platform
        {
            get
            {
#if UNITY_ANDROID
                return RuntimePlatform.Android;
#elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
#else
                return RuntimePlatform.WindowsPlayer;
#endif
            }
        }

    }
}
