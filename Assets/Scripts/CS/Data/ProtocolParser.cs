using System;
using System.Collections.Generic;
using Ap.Net;
using Msg;

/// <summary>
/// 这个类由系统自动产生
/// </summary>
public class ProtocolParser
{
    /// <summary>
    /// 解析后的回调
    /// </summary>
    /// <param name="data"></param>
    public delegate void OnParsedHandle(object data);

    /// <summary>
    /// 回调处理
    /// </summary>
    private static Dictionary<string, List<OnParsedHandle>> Handles
    {
        get
        {
            return m_Handles;
        }
    }

    private static Dictionary<string, List<OnParsedHandle>> m_Handles = new Dictionary<string, List<OnParsedHandle>>();
    /// <summary>
    /// 初始化
    /// </summary>
    public static void Init()
    {
        m_Handles.Add("msg.LOGIN_INFO", new List<OnParsedHandle>());
        m_Handles.Add("msg.COMMON_INFO", new List<OnParsedHandle>());
        m_Handles.Add("msg.PLAYER", new List<OnParsedHandle>());
    }

    // 增加监听
    public static void AddHandle(string msg, OnParsedHandle handle)
    {
        if (m_Handles.ContainsKey(msg))
        {

        }
        else
        {
            m_Handles.Add(msg, new List<OnParsedHandle>());
        }

        m_Handles[msg].Add(handle);
    }

    public static void RemoveHandle(string msg, OnParsedHandle handle)
    {
        if (m_Handles.ContainsKey(msg))
        {
            m_Handles[msg].Remove(handle);
        }
    }

    /// <summary>
    /// 统一解析处理函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    public static void Parse(int key, ByteBuffer data)
    {
        if (data == null)
            return;
        
        Byte[] bytes = data.ReadBytesToEnd();
        if (bytes == null || bytes.Length == 0)
            return;
        if (key == 0)
        {
            var value = ProtocolHelper.BytesToObject<LoginInfo>(bytes, 0, bytes.Length);
            OnParsed("Msg.LoginInfo", value);
        }
        else if (key == 1)
        {
            var value = ProtocolHelper.BytesToObject<CommonInfo>(bytes, 0, bytes.Length);
            OnParsed("Msg.CommonInfo", value);
        }
        else if (key == 2)
        {

        }

    }
    /// <summary>
    /// 触发回调
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="data"></param>
    private static void OnParsed(string msg, object data)
    {
        if (m_Handles.ContainsKey(msg))
        {
            for (int i = 0; i < m_Handles[msg].Count; i++)
            {
                try
                {
                    m_Handles[msg][i].Invoke(data);
                }
                catch (Exception ex)
                {
                    Ap.Tools.Logger.Instance.Write("ProtocolParser CallBack Error ! Msg: " + msg);
                }
            }
        }
        else
        {
            //Ap.Tools.Logger.Instance.Write("ProtocolParser Error ! Msg: " + msg);
        }
    }

    /// <summary>
    /// 发送登陆信息
    /// </summary>
    /// <param name="data"></param>
    public static void SendLOGIN_INFO(LoginInfo data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteShort(0);
        buffer.WriteBytes(ProtocolHelper.ObjectToBytes<LoginInfo>(data));

        Ap.Managers.NetworkManager.Instance.SendMessage(buffer);
    }
}

