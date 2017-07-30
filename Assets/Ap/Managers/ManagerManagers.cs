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
                if (File.Exists(MANAGERS_LOG))
                {
                    string text = FileHelper.ReadText(MANAGERS_LOG);
                    return text.Contains("INIT_SUCCESS");
                }
                return false;
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
            FileHelper.WriteText(MANAGERS_LOG, "START_INIT");

            // 拷贝 Assetbundle manifeast
            string from = "AssetBundles/" + Ap.Base.Environment.GetPlatformName();
            string to = Ap.Base.Environment.AssetBundlePath + "/" + Ap.Base.Environment.GetPlatformName();
            FileHelper.CopyFromStreamingAssets(from, to);
            FileHelper.WriteText(MANAGERS_LOG, "COPY ASSETBUNDLE MANIFEAST");

            // 解压脚本
            LZMACompress lzma = new LZMACompress();
            from = "Scripts/Lua.7z";
            to = Ap.Base.Environment.TempPath + "/Lua.7z";
            FileHelper.CopyFromStreamingAssets(from, to);
            from = Ap.Base.Environment.LuaPath + "/Lua.7z";
            lzma.Decompress(to, from);

            // TODO


            FileHelper.WriteText(MANAGERS_LOG, "INIT_SUCCESS");
            yield return null;
        }
    }
}
