using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 协议控制
/// </summary>
public class ProtocolCtr
{
    // 这里添加服务器协议解析后的数据处理
    public static void Init()
    {
        ProtocolParser.Handles["msg.LOGIN_INFO"].Add(StartUp.StartUp_OnLogin);
    }
}

