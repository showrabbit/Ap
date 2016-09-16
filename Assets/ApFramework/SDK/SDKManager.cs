using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.SDK
{
    public class SDKManager
    {
        /// <summary>
        /// 本次绑定的SDK
        /// </summary>
        public Dictionary<string, SDK> m_BindedSDK = new Dictionary<string, SDK>();

        public SDK Link
        {
            get
            {
                return m_Link;
            }
        }
        private SDK m_Link = null;

        /// <summary>
        /// 增加SDK
        /// </summary>
        /// <param name="name"></param>
        public void AddSDK(string name)
        {
            SDK sdk = SDKFactory.Create(name);
            sdk.OnLogin += SDK_OnLogin;
            sdk.OnLogout += SDK_OnLogout;
            sdk.OnPay += SDK_OnPay;
            sdk.OnShowAccountCenter += SDK_OnShowAccountCenter;
            sdk.OnSwitchLogin += SDK_OnSwitchLogin;
            m_BindedSDK.Add(name, sdk);
        }
        /// <summary>
        /// 移除SDK
        /// </summary>
        /// <param name="name"></param>
        public void RemoveSDK(string name)
        {
            m_BindedSDK.Remove(name);
        }

        /// <summary>
        /// 连接SDK
        /// </summary>
        /// <param name="name"></param>
        public void LinkSDK(string name)
        {
            m_Link = m_BindedSDK[name];
        }

        public void Login(string gameid,string name,string pwd, string p1,string p2)
        {
            m_Link.Login(gameid, name, pwd, p1, p2);
        }

        public void Logout(string sid)
        {
            m_Link.Logout(sid);
        }

        public void Pay(string gameid,string sid,string goodsid)
        {
            m_Link.Pay(gameid, sid, goodsid);
        }

        public void SwitchLogin()
        {
            m_Link.SwitchLogin();
        }

        public void ShowAccountCenter()
        {
            m_Link.ShowAccountCenter();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SDK_OnLogin(object sender,SDKLoginEventArgs e)
        {

        }

        private void SDK_OnLogout(object sender, SDKEventArgs e)
        {

        }

        private void SDK_OnPay(object sender, SDKPayEventArgs e)
        {

        }
        private void SDK_OnSwitchLogin(object sender,SDKEventArgs e)
        {

        }
        private void SDK_OnShowAccountCenter(object sender,SDKEventArgs e)
        {

        }
    }
}
