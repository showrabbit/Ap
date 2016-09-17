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
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 初始化
    /// 1.读取本地配置
    /// 2.加载管理模块
    /// 3.调用自动更新模块
    /// 4.调用lua启动模块
    /// 5.进入菜单界面
    /// 6.进入主界面
    /// </summary>
    void Init()
    {
        // 加载配置
        var c = Config.Instance;
        // 加载管理类
        var m = ManagerManagers.Instance;

        if( AutoUpdateCtr.IsAutoUpdate () )
        {
            // 显示更新界面
            // 更新界面包含AutoUpdate
        }
        else
        {

        }

        
    }
}



