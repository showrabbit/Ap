using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ap.Base;
using Ap.Net;
using Ap.UI;
using UnityEngine.UI;

public class MainFormCtr : MonoBehaviourEx
{
    public ButtonEx StartBtn;
    public ButtonEx LoginBtn;


    public void Start()
    {
        StartBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(StartBtn_OnClick));
        Ap.Managers.NetworkManager.Instance.OnReceivedMessage += NetworkManager_OnReceivedMessage;
        LoginBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(LoginBtn_OnClick));
        // test 
        ProtocolParser.AddHandle("Msg.CommonInfo", MainFormCtr_OnLogin);
    }

    private void OnDestroy()
    {
        //Ap.Managers.NetworkManager.Instance.OnReceivedMessage -= NetworkManager_OnReceivedMessage;
        ProtocolParser.RemoveHandle("Msg.CommonInfo", MainFormCtr_OnLogin);
    }



    /// <summary>
    /// 接收到网络消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    private void NetworkManager_OnReceivedMessage(int id, ByteBuffer data)
    {
        if (id == Ap.Net.Protocal.Connect)
        {
            // 连接成功
            print("connect");
        }
        else if (id == Ap.Net.Protocal.Disconnect)
        {
            // 断开
        }
    }

    private void StartBtn_OnClick()
    {

        Ap.Base.Context.Instance.ServerIp = "127.0.0.1";
        Ap.Base.Context.Instance.ServerPort = 3563;
        Ap.Managers.NetworkManager.Instance.SendConnect();
    }

    private void LoginBtn_OnClick()
    {
        Msg.LoginInfo info = new Msg.LoginInfo();
        info.Token = "111";
        info.UserName = "Dada";
        info.UserPwd = "Mom";
        ProtocolParser.SendLOGIN_INFO(info);
    }

    /// <summary>
    /// 监听登陆消息
    /// </summary>
    /// <param name="data"></param>
    public static void MainFormCtr_OnLogin(object data)
    {
        if (data == null)
            return;
        var info = data as Msg.CommonInfo;

        print("LoginInfo:" + info.Msg);
        if (info.Nums != null)
        {
            foreach (var v in info.Nums)
            {
                print("LoginInfo Nums:" + v.ToString());
            }
        }
    }
}

