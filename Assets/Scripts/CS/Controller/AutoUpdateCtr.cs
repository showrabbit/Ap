using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.Base;
using Ap.UI;
using Ap.Net;
using System;

public class AutoUpdateCtr : FormCtr
{
    /// <summary>
    /// 更新的文件信息
    /// </summary>
    public class FileInfo
    {
        public string Name;
        public int Size;
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
    public void Download()
    {
        StartCoroutine(DownloadAsync());
    }

    private IEnumerator DownloadAsync()
    {
        HttpClient server = new HttpClient();
        Dictionary<string, string> param = new Dictionary<string, string>();
        // 获取服务器更新列表
        param.Clear();
        param.Add("version", Config.Instance.GetLocalVersion().ToString());
        WWW www = server.PostWithWWW(Config.Instance.Data[Config.UPDATE_SERVER] + "//GetUpdateFiles", param);
        yield return www;
        List<FileInfo> files = GetUpdateFiles(www);
        if (files == null || files.Count == 0)
        {
            //todo error
            yield break;
        }
        // 读取下载进度

        // 下载更新每个文件


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
            // 格式: filename;size|filename;size
            string con = System.Text.Encoding.Default.GetString(www.bytes);
            string[] splits = con.Split('|');
            List<FileInfo> files = new List<FileInfo>();
            for (int i = 0; i < splits.Length; i++)
            {
                string[] subSplits = splits[i].Split(';');
                FileInfo f = new FileInfo();
                f.Name = subSplits[0];
                f.Size = Convert.ToInt32(subSplits[1]);
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
