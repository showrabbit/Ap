﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Ap_Lua_PointerClickEventWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Ap.Lua.PointerClickEvent), typeof(Ap.Lua.ControlEvent));
		L.RegFunction("Create", Create);
		L.RegFunction("New", _CreateAp_Lua_PointerClickEvent);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Fun", get_Fun, set_Fun);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAp_Lua_PointerClickEvent(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				Ap.UI.ButtonEx arg0 = (Ap.UI.ButtonEx)ToLua.CheckObject<Ap.UI.ButtonEx>(L, 1);
				Ap.Lua.PointerClickEvent obj = new Ap.Lua.PointerClickEvent(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Ap.Lua.PointerClickEvent.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Create(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				Ap.UI.ButtonEx arg0 = (Ap.UI.ButtonEx)ToLua.CheckObject<Ap.UI.ButtonEx>(L, 1);
				Ap.Lua.PointerClickEvent o = Ap.Lua.PointerClickEvent.Create(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 2)
			{
				Ap.UI.ButtonEx arg0 = (Ap.UI.ButtonEx)ToLua.CheckObject<Ap.UI.ButtonEx>(L, 1);
				LuaFunction arg1 = ToLua.CheckLuaFunction(L, 2);
				Ap.Lua.PointerClickEvent o = Ap.Lua.PointerClickEvent.Create(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Ap.Lua.PointerClickEvent.Create");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Fun(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Ap.Lua.PointerClickEvent obj = (Ap.Lua.PointerClickEvent)o;
			LuaInterface.LuaFunction ret = obj.Fun;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Fun on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Fun(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Ap.Lua.PointerClickEvent obj = (Ap.Lua.PointerClickEvent)o;
			LuaFunction arg0 = ToLua.CheckLuaFunction(L, 2);
			obj.Fun = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Fun on a nil value");
		}
	}
}

