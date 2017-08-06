using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Base
{
    // 系统部分 1 - 1000
    public partial class EventTypes
    {

        // Game 1 -- 50
        public const int GameStart = 1;
        public const int GamePause = 2;
        public const int GameOver = 3;


        // SDK 100 - 150
        public const int SdkLogined = 100;
        public const int SdkLogouted = 101;
        public const int SdkSwitchLogined = 102;
        public const int SdkShowAccountCentered = 103;
        public const int SdkPayed = 104;
        public const int SdkInited = 105;

        public const int SdkLogin = 110;
        public const int SdkLogout = 111;
        public const int SdkSwitchLogin = 112;
        public const int SdkShowAccountCenter = 113;
        public const int SdkPay = 114;
        public const int SdkInit = 115;

        // 游戏释放 151 - 180
        public const int GameInitStart = 151;
        // 初始化游戏目录
        public const int GameInitDirectory = 152;
        // 初始化mainfeast
        public const int GameInitManifeast = 153;
        // 初始化lua脚本
        public const int GameInitLua = 154;
        // 初始配置
        public const int GameInitData = 155;

        public const int GameInitEnd = 180;

    }

}

