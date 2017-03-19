using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ap.SDK
{
    public abstract class SdkBase
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
        public abstract void Login(string param);

        public abstract void Logout(string param);

        public abstract void Pay(string param);

        public abstract void SwitchLogin(string param);

        public abstract void ShowAccountCenter(string param);
    }
}
