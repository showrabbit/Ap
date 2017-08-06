using System;
using System.Collections.Generic;
using Ap.Base;
using Ap.Tools;
using System.IO;
using System.Collections;

namespace Ap.Managers
{
    public class ManagerManagers : SingletonBase<ManagerManagers>
    {
        public static string MANAGERS_LOG = Ap.Base.Environment.TempPath + "//MANAGERS_LOG.log";

        public AssetBundleManager A
        {
            get
            {
                return AssetBundleManager.Instance;
            }
        }

        public EventManager E
        {
            get
            {
                return EventManager.Instance;
            }
        }

        public FormManager F
        {
            get
            {
                return FormManager.Instance;
            }
        }

        public GameManager G
        {
            get
            {
                return GameManager.Instance;
            }
        }

        public LuaManager L
        {
            get
            {
                return LuaManager.Instance;
            }
        }

        public NetworkManager N
        {
            get
            {
                return NetworkManager.Instance;
            }
        }

        public ObjectPoolManager O
        {
            get
            {
                return ObjectPoolManager.Instance;
            }
        }

        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool IsFirstInited
        {
            get
            {
#if UNITY_EDITOR
                return false;

#else
                if (File.Exists(MANAGERS_LOG))
                {
                    string text = FileHelper.ReadText(MANAGERS_LOG);
                    return text.Contains("INIT_SUCCESS");
                }
                return true;
#endif
            }
        }

        protected override void Init()
        {
            var e = EventManager.Instance;
            var a = AssetBundleManager.Instance;
            var f = FormManager.Instance;
            var g = GameManager.Instance;
            var l = LuaManager.Instance;
            var n = NetworkManager.Instance;
            var o = ObjectPoolManager.Instance;
        }

        public override void Clear()
        {

        }

        /// <summary>
        /// 首次初始化
        /// </summary>
        /// <returns></returns>
        public IEnumerator ExecFirstInit()
        {
            EventData data = new EventData();

            data = new EventData();
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitStart, this, data);

            FileHelper.WriteText(MANAGERS_LOG, "START_INIT");


            // 创建各个初始目录
            // 存放lua脚本目录
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.LuaPath);
            // 存放配置
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.DataPath);
            // 存放缓存
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.CachePath);
            // 存放日志
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.LogPath);
            // 存放更新后的assetbundles
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.AssetBundleUpdatePath);
            // 临时文件夹
            DirectoryHelper.CreateDirectory(Ap.Base.Environment.TempPath);
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitDirectory, this, data);

            // 拷贝 Assetbundle manifeast
            string from = "AssetBundles/" + Ap.Base.Environment.GetPlatformName();
            string to = Ap.Base.Environment.AssetBundleUpdatePath + "/" + Ap.Base.Environment.GetPlatformName();
            FileHelper.CopyFromStreamingAssets(from, to);
            FileHelper.WriteText(MANAGERS_LOG, "COPY ASSETBUNDLE MANIFEAST");

            data = new EventData();
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitManifeast, this, data);


            // 解压脚本
            LZMACompress lzma = new LZMACompress();
            from = "Scripts/Lua.7z";
            to = Ap.Base.Environment.TempPath + "/Lua.7z";
            FileHelper.CopyFromStreamingAssets(from, to);
            from = Ap.Base.Environment.LuaPath + "/Lua";
            lzma.Decompress(to, from);
            FileHelper.DeleteFile(to);

            from = "Scripts/ToLua.7z";
            to = Ap.Base.Environment.TempPath + "/ToLua.7z";
            FileHelper.CopyFromStreamingAssets(from, to);
            from = Ap.Base.Environment.LuaPath + "/ToLua";
            lzma.Decompress(to, from);
            FileHelper.DeleteFile(to);

            FileHelper.WriteText(MANAGERS_LOG, "COPY LUA SCRIPT");
            data = new EventData();
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitLua, this, data);

            // 拷贝配置文件
            FileHelper.CopyTextFromResources("Data/AssetBundles", Ap.Base.Environment.DataPath + "/AssetBundles.txt");
            FileHelper.CopyTextFromResources("Data/AssetBundles", Ap.Base.Environment.DataPath + "/Assets.txt");
            FileHelper.WriteText(MANAGERS_LOG, "COPY DATA");
            data = new EventData();
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitManifeast, this, data);

            FileHelper.WriteText(MANAGERS_LOG, "INIT_SUCCESS");

            data = new EventData();
            data.Values = new object[] { true };
            EventManager.Instance.Trigge(EventTypes.GameInitEnd, this, data);
            yield return null;
        }
    }
}
