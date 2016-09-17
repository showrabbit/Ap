using UnityEngine;
using System.Collections;
using Ap.Base;
using Ap.UI;

public class AutoUpdateCtr : FormCtr
{

    

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
        yield return null;
    }

    /// <summary>
    /// 是否需要自动更新
    /// </summary>
    /// <returns></returns>
    public static bool IsAutoUpdate()
    {
        /// todo
        /// 1. 从版本服务器读取版本号
        /// 2. 比较本地版本号 是否更新
        return false;
    }
}
