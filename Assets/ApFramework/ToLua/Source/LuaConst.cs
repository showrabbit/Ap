
using Ap.Tools;
using UnityEngine;

public static partial class LuaConst
{
    public static string LuaDir = Utility.FrameworkPath + "/Lua/";
    public static string ToluaDir = Utility.FrameworkPath + "/ToLua/Lua";
#if UNITY_STANDALONE
    public static string OsDir = "Win";
#elif UNITY_ANDROID
    public static string OsDir = "Android";            
#elif UNITY_IPHONE
    public static string OsDir = "iOS";        
#else
    public static string OsDir = "";        
#endif

    public static bool OpenLuaSocket = true;            //是否打开Lua Socket库
}