using UnityEngine;
using System.Collections;
using Ap.Base;
using Ap.Managers;
/// <summary>
/// 启动类
/// </summary>
public class StartUp : MonoBehaviourEx
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 初始化
    /// 1.读取本地配置
    /// 2.加载管理模块(更新资源/LUA)
    /// 3.调用自动更新模块
    /// 4.调用lua启动模块
    /// 5.进入菜单界面
    /// 6.进入主界面
    /// </summary>
    IEnumerator Init()
    {
        // 加载配置
        var c = Config.Instance;
        // 加载管理类
        var m = ManagerManagers.Instance;
        // 延迟一帧
        yield return new WaitForEndOfFrame();

        // 是否初次加载游戏
        if (Ap.Managers.ManagerManagers.Instance.IsFirstInited)
        {
            yield return ManagerManagers.Instance.ExecFirstInit();
        }

        // 确保加载了assetbundle
        yield return AssetBundleManager.Instance.AssetBundleManifestOpt;

        // 注册协议内容
        ProtocolParser.Init();
        m.N.OnReceivedMessage += ProtocolParser.Parse;
        ProtocolCtr.Init();
        
        if (AutoUpdateCtr.IsAutoUpdate())
        {
            EventManager.Instance.AddHandle(EventTypes.AutoUpdate, AutoUpdateEnd);
            // 显示更新界面
            // 更新界面包含AutoUpdate
            // 更新结束后重新加载资源
            ManagerManagers.Instance.F.Show("AutoUpdate");
        }
        else
        {
            // 不需要自动更新/更新完毕的
            // 加载Lua 加载lua 
            // 加载地图
            ManagerManagers.Instance.L.StartMain();

        }
        
        yield return null;
    }



    /// <summary>
    /// 自动更新结束
    /// </summary>
    private void AutoUpdateEnd(object sender, EventData e)
    {
        // 清理游戏
        // 重新开始游戏
    }

    /// <summary>
    /// 监听登陆消息
    /// </summary>
    /// <param name="data"></param>
    public static void StartUp_OnLogin(object data)
    {
        if (data == null)
            return;
        var info = data as Msg.LoginInfo;

    }
}



