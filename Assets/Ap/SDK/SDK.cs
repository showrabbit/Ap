using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ap.SDK
{
    public abstract class SDK
    {

        public delegate void LoginHandle(object sender,SDKLoginEventArgs e);
        public delegate void LogoutHandle(object sender, SDKEventArgs e);
        public delegate void PayHandle(object sender, SDKPayEventArgs e);
        public delegate void SwitchLoginHandle(object sender, SDKEventArgs e);
        public delegate void ShowAccountCenterHandle(object sender, SDKEventArgs e);

        public LoginHandle OnLogin;
        public LogoutHandle OnLogout;
        public PayHandle OnPay;
        public SwitchLoginHandle OnSwitchLogin;
        public ShowAccountCenterHandle OnShowAccountCenter;
        public abstract void Login(string gameid, string name, string pwd, string p1, string p2);

        public abstract void Logout(string sid);

        public abstract void Pay(string gameid, string sid, string goodsid);

        public abstract void SwitchLogin();

        public abstract void ShowAccountCenter();
    }
}
