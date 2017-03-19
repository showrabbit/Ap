using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ap.Tools
{
    public class Utility
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
                if(Application.isEditor)
                {
                    return Application.dataPath + "/Resources/Data";
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
                return "Data Source=" + Utility.DataPath + "/db.txt"; ;
            }
        }
        /// <summary>
        /// 本地用户数据路径
        /// </summary>
        public static string DbUserPath
        {
            get
            {
                return "Data Source=" + Utility.DataPath + "/user.txt"; ;
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
                return Application.dataPath + "/Scripts/Lua";
#else
                return Application.persistentDataPath + "/Scripts/Lua";
#endif
            }
        }
        
        public static string FrameworkPath
        {
            get
            {
                return Application.dataPath + "/Ap";
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

        /// <summary>
        /// 清楚对象的子对象
        /// </summary>
        public static void GameObjectDestroyChild(GameObject obj, int startIndex = 0)
        {
            for (int tmpi = startIndex; tmpi < obj.transform.childCount - 1; tmpi++)
            {
                GameObject.Destroy(obj.transform.GetChild(tmpi).gameObject);
            }
        }
    }

}
