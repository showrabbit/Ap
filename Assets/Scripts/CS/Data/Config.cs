using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.Base;
using Ap.Tools;
/// <summary>
/// 本地配置数据
/// </summary>
public class Config : SingletonBase<Config>
{
    
    public Dictionary<string,string> Data
    {
        get
        {
            return m_Data;
        }
    }
    private Dictionary<string, string> m_Data = new Dictionary<string, string>();

    private string m_Path = Utility.DataPath + "/config.txt";

    protected override void Init()
    {
        FileTable ft = new FileTable(m_Path);
        foreach(var r in ft.Rows)
        {
            string k = r.Value[0];
            string v = r.Value[1];
            m_Data.Add(k, v);
        }
    }

    public override void Clear()
    {
        
    }
}
