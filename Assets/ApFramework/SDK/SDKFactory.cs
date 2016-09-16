using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.SDK
{
    public class SDKFactory
    {
        public const string QQ = "QQ";
        public const string WEIBO = "WEIBO";

        public static SDK Create(string name)
        {
            SDK sdk = null;
#if ANROID

#elif IOS

#endif
            return sdk;
        }
    }
}
