using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Core
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
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSX:
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

        private static string m_DataPath = "";
        /// <summary>
        /// 取得数据配置存放目录
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
                {
                    if (string.IsNullOrEmpty(m_DataPath))
                        m_DataPath = Application.persistentDataPath + "/Data/";
                    return m_DataPath;
                }
            }
        }

        private static string m_CachePath = "";
        /// <summary>
        /// 缓存路径
        /// </summary>
        public static string CachePath
        {
            get
            {
                if (Application.isEditor)
                {
                    if (string.IsNullOrEmpty(m_CachePath))
                    {
                        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
                        path = path + "/Cache/";
                        m_CachePath = path;
                    }
                    return m_CachePath;
                }
                else
                {
                    if (string.IsNullOrEmpty(m_CachePath))
                        m_CachePath = Application.persistentDataPath + "/Data/";
                    return m_CachePath;
                }
            }
        }

        private static string m_AssetBundleUpdatePath = "";
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
                    if (string.IsNullOrEmpty(m_AssetBundleUpdatePath))
                        m_AssetBundleUpdatePath = Application.persistentDataPath + "/AssetBundles/";
                    return m_AssetBundleUpdatePath;
                }
            }
        }

        private static string m_AssetBundlePath;
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
                    if (string.IsNullOrEmpty(m_AssetBundlePath))
                        m_AssetBundlePath = Application.streamingAssetsPath + "/AssetBundles/";
                    return m_AssetBundlePath;
                }
            }
        }

        private static string m_LuaPath;
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
                if (string.IsNullOrEmpty(m_LuaPath))
                    m_LuaPath = Application.persistentDataPath + "/Scripts/Lua/";
                return m_LuaPath;
#endif
            }
        }
        
        private static string m_LogPath;
        /// <summary>
        /// 游戏运行中的日志文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
#if UNITY_EDITOR
                if (string.IsNullOrEmpty(m_LogPath))
                {
                    string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
                    path = path + "/Log/";
                    m_LogPath = path;
                }
                return m_LogPath;
#else
                if (string.IsNullOrEmpty(m_LogPath))
                    m_LogPath = Application.persistentDataPath + "/Log/";
                return m_LogPath;
#endif
            }
        }

        private static string m_TempPath;
        /// <summary>
        /// 游戏运行中的临时文件夹
        /// </summary>
        public static string TempPath
        {
            get
            {
#if UNITY_EDITOR
                if (string.IsNullOrEmpty(m_TempPath))
                {
                    string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
                    path = path + "/Temp/";
                    m_TempPath = path;
                }
                return m_TempPath;
#else
                if (string.IsNullOrEmpty(m_TempPath))
                    m_TempPath = Application.persistentDataPath + "/Temp/";
                return m_TempPath;
#endif
            }
        }
        /// <summary>
        /// 框架的目录
        /// </summary>
        public static string FrameworkPath
        {
            get
            {
                return Application.dataPath + "/Ap/";
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
