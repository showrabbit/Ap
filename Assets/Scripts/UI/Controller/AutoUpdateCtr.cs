using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.Core;
using Ap.UI;
using Ap.Net;
using Ap.Tools;
using System;
using System.IO;
using Ap.UI.Core;
using Ap.Res;

/// <summary>
/// 自动更新控制
/// </summary>
public class AutoUpdateCtr : FormCtr
{

    /// <summary>
    /// 更新的文件信息
    /// </summary>
    public class FileInfo
    {
        public ResTypeEnum ResType;
        public string Name;
        // 相对更新路径
        public string Path;
        public int Size;
        public string MD5;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 下载
    /// </summary>
    public void Download(string serverVersion)
    {
        StartCoroutine(DownloadAsync(serverVersion));
    }

    private IEnumerator DownloadAsync(string serverVersion)
    {
        HttpClient server = new HttpClient();
        Dictionary<string, string> param = new Dictionary<string, string>();
        // 获取服务器更新列表
        param.Clear();
        param.Add("version", Config.Instance.GetLocalVersion().ToString());
        WWW www = server.PostWithWWW(Config.Instance.Data[Config.UPDATE_SERVER] + "//GetUpdateFiles", param);
        yield return www;

        if (string.IsNullOrEmpty(www.error) == false)
        {
            // todo 记录错误日志
            yield break;
        }


        List<FileInfo> files = GetUpdateFiles(www);

        if (files == null || files.Count == 0)
        {
            //todo error
            yield break;
        }
        // 创建临时缓存目录
        string tempPath = Ap.Core.Environment.TempPath + "/" + serverVersion;
        Ap.Tools.DirectoryHelper.CreateDirectory(tempPath);

        // 需要支持断点续传
        // 目前支持按文件
        // 读取下载进度

        // 获取临时目录下面的所有文件
        Dictionary<string, AutoUpdateLog> downloadMd5 = new Dictionary<string, AutoUpdateLog>();
        foreach (var f in System.IO.Directory.GetFiles(tempPath))
        {
            string md5 = FileHelper.GetFileMD5(f);
            string fileName = FileHelper.GetFileName(f);
            AutoUpdateLog log = new AutoUpdateLog();
            log.FileName = fileName;
            log.MD5 = md5;
            downloadMd5.Add(fileName, log);
        }

        // 获取最终要更新的文件列表
        List<FileInfo> downloadFiles = new List<FileInfo>();
        for (int i = 0; i < files.Count; i++)
        {
            if (downloadMd5.ContainsKey(files[i].Name))
            {
                if (downloadMd5[files[i].Name].MD5 == files[i].MD5)
                {
                    //files.RemoveAt(i);
                    //i--;
                }
                else
                {
                    downloadFiles.Add(files[i]);
                }
            }
        }

        // 下载更新每个文件
        // 继续完成下载单个文件
        for (int i = 0; i < files.Count; i++)
        {

            www = DownloadFile(server, files[i].Name);
            yield return www;
            if (string.IsNullOrEmpty(www.error) == false)
            {
                // todo 错误处理
                yield break;
            }
            else
            {
                string path = tempPath + "/" + files[i].Name;
                Ap.Tools.FileHelper.CreateOrReplace(path, www.bytes);
            }
        }

        // 下载都成功后
        // 把对应文件挪到对应的目录里面
        for (int i = 0; i < files.Count; i++)
        {
            CopyFile(files[i], tempPath);
        }

        // 拷贝也成功后
        // 清理临时文件夹
        // 确定都成功后才清理
        for (int i = 0; i < files.Count; i++)
        {
            string path = tempPath + "/" + files[i].Name;
            if (File.Exists(path))
                File.Delete(path);
        }

        // 更新结束

        yield return null;
    }

    /// <summary>
    /// 获取更新的文件列表
    /// </summary>
    /// <returns></returns>
    private List<FileInfo> GetUpdateFiles(WWW www)
    {
        if (string.IsNullOrEmpty(www.error))
        {
            // 格式: restype;filename;path;size;md5|restype;filename;path;size;md5
            string con = System.Text.Encoding.Default.GetString(www.bytes);
            string[] splits = con.Split('|');
            List<FileInfo> files = new List<FileInfo>();
            for (int i = 0; i < splits.Length; i++)
            {
                string[] subSplits = splits[i].Split(';');
                FileInfo f = new FileInfo();
                f.ResType = (ResTypeEnum)Convert.ToInt32(subSplits[0]);
                f.Name = subSplits[1];
                f.Path = subSplits[2];
                f.Size = Convert.ToInt32(subSplits[3]);
                f.MD5 = subSplits[4];
                files.Add(f);
            }
            return files;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    private WWW DownloadFile(HttpClient server, string path)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("path", path);
        WWW www = server.PostWithWWW(Config.Instance.Data[Config.UPDATE_SERVER] + "//DownloadFile", param);

        return www;
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="info"></param>
    private void CopyFile(FileInfo info, string tempPath)
    {
        string path = tempPath + "/" + info.Name;
        if (File.Exists(path))
        {
            switch (info.ResType)
            {
                case ResTypeEnum.AssetBundle:
                    {
                        string toPath = Ap.Core.Environment.AssetBundleUpdatePath + "/" + info.Path;
                        File.Copy(path, toPath, true);
                    }
                    break;
                case ResTypeEnum.LuaScript:
                    {
                        string toPath = Ap.Core.Environment.LuaPath + "/" + info.Path;
                        File.Copy(path, toPath, true);
                    }
                    break;
            }

        }
    }

    /// <summary>
    /// 是否需要自动更新
    /// </summary>
    /// <returns></returns>
    public static bool IsAutoUpdate()
    {
#if UNITY_EDITOR
        return false;

#else
        /// 1. 从版本服务器读取版本号
        /// 2. 比较本地版本号 是否更新
        Version serverVersion = Config.Instance.GetServerVersion();
        Version localVersion = Config.Instance.GetLocalVersion();
        if (serverVersion.CompareTo(localVersion) > 0)
        {
            return true;
        }
        else
            return false;
#endif
    }


}
