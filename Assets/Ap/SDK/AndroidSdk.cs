using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Core;
using Ap.Event;

namespace Ap.SDK
{
    public class AndroidSDK : SdkBase
    {
        public override void Login(string param)
        {
            AndroidMsgManager.Instance.AndroidSendMessage(EventTypes.SdkLogin, param);
        }

        public override void Logout(string param)
        {
            AndroidMsgManager.Instance.AndroidSendMessage(EventTypes.SdkLogout, param);
        }

        public override void Pay(string param)
        {
            AndroidMsgManager.Instance.AndroidSendMessage(EventTypes.SdkPay, param);
        }

        public override void SwitchLogin(string param)
        {
            AndroidMsgManager.Instance.AndroidSendMessage(EventTypes.SdkSwitchLogin, param);
        }

        public override void ShowAccountCenter(string param)
        {
            AndroidMsgManager.Instance.AndroidSendMessage(EventTypes.SdkShowAccountCenter, param);
        }
    }
}
