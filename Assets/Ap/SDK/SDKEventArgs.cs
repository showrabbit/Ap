using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.SDK
{
    public class SDKEventArgs
    {
        public bool IsError;
        public string ErrorInfo;
    }

    public class SDKLoginEventArgs : SDKEventArgs
    {
        public string SID;
    }

    public class SDKPayEventArgs : SDKEventArgs
    {
        public string OrderID;
    }

}
