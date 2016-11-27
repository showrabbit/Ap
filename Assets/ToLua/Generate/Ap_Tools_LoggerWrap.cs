﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Ap_Tools_LoggerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Ap.Tools.Logger), typeof(Ap.Base.SingletonBase<Ap.Tools.Logger>));
		L.RegFunction("Clear", Clear);
		L.RegFunction("Write", Write);
		L.RegFunction("New", _CreateAp_Tools_Logger);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAp_Tools_Logger(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Ap.Tools.Logger obj = new Ap.Tools.Logger();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Ap.Tools.Logger.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Ap.Tools.Logger obj = (Ap.Tools.Logger)ToLua.CheckObject(L, 1, typeof(Ap.Tools.Logger));
			obj.Clear();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Write(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(Ap.Tools.Logger), typeof(string)))
			{
				Ap.Tools.Logger obj = (Ap.Tools.Logger)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				obj.Write(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ap.Tools.Logger), typeof(int), typeof(string)))
			{
				Ap.Tools.Logger obj = (Ap.Tools.Logger)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				obj.Write(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(Ap.Tools.Logger), typeof(Ap.Tools.LogLevel), typeof(string)))
			{
				Ap.Tools.Logger obj = (Ap.Tools.Logger)ToLua.ToObject(L, 1);
				Ap.Tools.LogLevel arg0 = (Ap.Tools.LogLevel)ToLua.ToObject(L, 2);
				string arg1 = ToLua.ToString(L, 3);
				obj.Write(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Ap.Tools.Logger.Write");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

