﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Ap_Base_SingletonBase_Ap_Managers_ManagerManagersWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Ap.Base.SingletonBase<Ap.Managers.ManagerManagers>), typeof(System.Object), "SingletonBase_Ap_Managers_ManagerManagers");
		L.RegFunction("Clear", Clear);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Ap.Base.SingletonBase<Ap.Managers.ManagerManagers> obj = (Ap.Base.SingletonBase<Ap.Managers.ManagerManagers>)ToLua.CheckObject<Ap.Base.SingletonBase<Ap.Managers.ManagerManagers>>(L, 1);
			obj.Clear();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, Ap.Base.SingletonBase<Ap.Managers.ManagerManagers>.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

