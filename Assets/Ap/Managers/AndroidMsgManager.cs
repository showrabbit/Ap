using System;
using UnityEngine;

namespace Ap.Managers
{
    public class AndroidMsgManager : ManagerBase<AndroidMsgManager>
    {
        //private UnityEngine.ando

        protected override void Init()
        {
            
        }

        

        /// <summary>
        /// 发送消息到android
        /// </summary>
        /// <param name="type"></param>
        /// <param name="param"></param>
        public void AndroidSendMessage(int type,string param)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject obj = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    obj.Call("SendMsg", type, param);
                }
            }
        }

        /// <summary>
        /// 接收到来自Android的消息
        /// </summary>
        /// <param name="msg"></param>
        public void AndroidAcceptMessage(string msg)
        {
            // 转成成标准的unity事件 分发出去
            string[] splits = msg.Split(new string[1] { "@#" },StringSplitOptions.RemoveEmptyEntries);
            int type = Convert.ToInt32(splits[0]);
            string param = splits[1];

            EventManager.Instance.Trigge(type, this, new Base.EventData(param));
        }
    }
}
