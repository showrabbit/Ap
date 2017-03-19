using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Managers;
using UnityEngine;
using Ap.Base;
using Ap.SDK;
namespace Ap.Managers
{
    public class SdkManager : ManagerBase<SdkManager>
    {
        public SdkBase Link
        {
            get
            {
                return m_Link;
            }
        }
        private SdkBase m_Link = null;

        protected override void Init()
        {
            switch (Ap.Tools.Utility.Platform)
            {
                case RuntimePlatform.Android:
                    m_Link = new AndroidSDK();
                    break;
                case RuntimePlatform.IPhonePlayer:
                    m_Link = new IosSdk();
                    break;
            }
            EventManager.Instance.AddHandle(EventTypes.SdkLogined, SDK_OnLogined);
            EventManager.Instance.AddHandle(EventTypes.SdkLogouted, SDK_OnLogouted);
            EventManager.Instance.AddHandle(EventTypes.SdkPayed, SDK_OnPayed);
            EventManager.Instance.AddHandle(EventTypes.SdkShowAccountCentered, SDK_OnShowAccountCentered);
            EventManager.Instance.AddHandle(EventTypes.SdkSwitchLogined, SDK_OnSwitchLogined);
        }

        public void Login(string param)
        {
            m_Link.Login(param);
        }

        public void Logout(string param)
        {
            m_Link.Logout(param);
        }

        public void Pay(string param)
        {
            m_Link.Pay(param);
        }

        public void SwitchLogin(string param)
        {
            m_Link.SwitchLogin(param);
        }

        public void ShowAccountCenter(string param)
        {
            m_Link.ShowAccountCenter(param);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SDK_OnLogined(object sender, EventData e)
        {
            //TODO 解析出里面的结果
        }

        private void SDK_OnLogouted(object sender, EventData e)
        {
            //TODO 解析出里面的结果
        }

        private void SDK_OnPayed(object sender, EventData e)
        {
            //TODO 解析出里面的结果
        }
        private void SDK_OnSwitchLogined(object sender, EventData e)
        {
            //TODO 解析出里面的结果
        }
        private void SDK_OnShowAccountCentered(object sender, EventData e)
        {
            //TODO 解析出里面的结果
        }


    }
}
