using Ap.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Ap.Pack
{
    public class PackageTools
    {
        public static void PackAssetBundle(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            // 重命名
            if (File.Exists(path + "/" + PackageConfig.Version))
            {
                if (File.Exists(path + "/" + Ap.Base.Environment.GetPlatformName()))
                    File.Delete(path + "/" + Ap.Base.Environment.GetPlatformName());
                File.Move(path + "/" + PackageConfig.Version, path + "/" + Ap.Base.Environment.GetPlatformName());
            }
            // 拷贝
            string toPath = Application.streamingAssetsPath + "/AssetBundles";
            if (Directory.Exists(toPath))
            {
                Directory.Delete(toPath, true);
            }
            Directory.CreateDirectory(toPath);

            foreach (string f in Directory.GetFiles(path))
            {
                if (f.ToLower().Contains("manifest"))
                {
                    continue;
                }

                string file = f.Replace("\\", "/");
                file = file.Substring(file.LastIndexOf("/"));
                File.Copy(f, toPath + "/" + file);
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 打包lua 
        /// </summary>
        /// <param name="outPath">打包输出的路径 这个路径会被先删除在创建!</param>
        public static void PackLua(string outPath)
        {
            // 创建目录
            if (System.IO.Directory.Exists(outPath))
            {
                System.IO.Directory.Delete(outPath, true);
            }
            // 拷贝文件
            PackageTools.DirectoryCopy(PackageConfig.ToLuaPath, outPath + "/ToLua");
            PackageTools.DirectoryCopy(PackageConfig.LuaPath, outPath + "/Lua");

            // luajit/luac

            Ap.Base.LZMACompress lzma = new Base.LZMACompress();
            lzma.CompressFolder(outPath + "/ToLua", outPath + "/ToLua.7z");
            lzma.CompressFolder(outPath + "/Lua", outPath + "/Lua.7z");

            System.IO.Directory.Delete(outPath + "/ToLua",true);
            System.IO.Directory.Delete(outPath + "/Lua", true);

            AssetDatabase.Refresh();
        }
        
        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static void DirectoryCopy(string fromPath, string toPath)
        {
            if (!Directory.Exists(fromPath))
            {
                throw new Exception("FromPath Error");
            }
            if (Directory.Exists(toPath))
            {
                throw new Exception("ToPath Error");
            }

            Directory.CreateDirectory(toPath);

            foreach (string f in Directory.GetFiles(fromPath))
            {
                string file = f.Replace("\\", "/");
                file = file.Substring(file.LastIndexOf("/"));
                File.Copy(f, toPath + "/" + file);
            }

            foreach (var dir in Directory.GetDirectories(fromPath))
            {
                string path = dir.Replace("\\", "/");
                path = toPath + "/" + path.Substring(path.LastIndexOf("/"));
                DirectoryCopy(dir, path);
            }

        }

        public static string[] GetLevelsFromBuildSettings()
        {
            List<string> levels = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                    levels.Add(EditorBuildSettings.scenes[i].path);
            }

            return levels.ToArray();
        }

    }
}
