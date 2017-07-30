using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.Base;
using Ap.Tools;
using Ap.Net;
using System;
/// <summary>
/// 本地配置数据
/// </summary>
public class Config : SingletonBase<Config>
{
    public static string VERSION = "VERSION";
    public static string UPDATE_SERVER = "UPDATE_SERVER";
    public static string LOGIN_SERVER = "LOGIN_SERVER";
    public static string GAME_SERVER = "GAME_SERVER";

    public Dictionary<string, string> Data
    {
        get
        {
            return m_Data;
        }
    }
    private Dictionary<string, string> m_Data = new Dictionary<string, string>();

    private string m_Path = Ap.Base.Environment.DataPath + "/config.txt";

    protected override void Init()
    {
        FileTable ft = new FileTable(m_Path);
        foreach (var r in ft.Rows)
        {
            string k = r.Value[0];
            string v = r.Value[1];
            m_Data.Add(k, v);
        }
    }

    public override void Clear()
    {

    }
    /// <summary>
    /// 保存配置内容
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Save(string key,string value)
    {

    }

    /// <summary>
    /// 获取服务器端的版本号
    /// </summary>
    /// <returns></returns>
    public Version GetServerVersion()
    {
        HttpClient http = new HttpClient();
        byte[] bytes = http.Get(Config.Instance.Data[Config.UPDATE_SERVER]+"//GetVersiion");
        if (bytes != null)
        {
            string value = System.Text.ASCIIEncoding.Default.GetString(bytes);
            Version version = new Version(value);
            return version;
        }
        else
            return null;
    }

    /// <summary>
    /// 获取本地配置
    /// </summary>
    /// <returns></returns>
    public Version GetLocalVersion()
    {
        Version version = new Version(this.Data[VERSION]);
        return version;
    }
}
