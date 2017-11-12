﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using System.Collections.Generic;
using LuaInterface;

public class DelegateFactory
{
	public delegate Delegate DelegateCreate(LuaFunction func, LuaTable self, bool flag);
	public static Dictionary<Type, DelegateCreate> dict = new Dictionary<Type, DelegateCreate>();
	static DelegateFactory factory = new DelegateFactory();

	public static void Init()
	{
		Register();
	}

	public static void Register()
	{
		dict.Clear();
		dict.Add(typeof(System.Action), factory.System_Action);
		dict.Add(typeof(UnityEngine.Events.UnityAction), factory.UnityEngine_Events_UnityAction);
		dict.Add(typeof(System.Predicate<int>), factory.System_Predicate_int);
		dict.Add(typeof(System.Action<int>), factory.System_Action_int);
		dict.Add(typeof(System.Comparison<int>), factory.System_Comparison_int);
		dict.Add(typeof(System.Func<int,int>), factory.System_Func_int_int);
		dict.Add(typeof(UnityEngine.Camera.CameraCallback), factory.UnityEngine_Camera_CameraCallback);
		dict.Add(typeof(UnityEngine.Application.LowMemoryCallback), factory.UnityEngine_Application_LowMemoryCallback);
		dict.Add(typeof(UnityEngine.Application.AdvertisingIdentifierCallback), factory.UnityEngine_Application_AdvertisingIdentifierCallback);
		dict.Add(typeof(UnityEngine.Application.LogCallback), factory.UnityEngine_Application_LogCallback);
		dict.Add(typeof(UnityEngine.AudioClip.PCMReaderCallback), factory.UnityEngine_AudioClip_PCMReaderCallback);
		dict.Add(typeof(UnityEngine.AudioClip.PCMSetPositionCallback), factory.UnityEngine_AudioClip_PCMSetPositionCallback);
		dict.Add(typeof(Ap.UI.Control.LoadAssetStartHandle), factory.Ap_UI_Control_LoadAssetStartHandle);
		dict.Add(typeof(Ap.UI.Control.LoadAssetEndHandle), factory.Ap_UI_Control_LoadAssetEndHandle);
		dict.Add(typeof(Ap.UI.Form.LoadHandle), factory.Ap_UI_Form_LoadHandle);
		dict.Add(typeof(Ap.UI.Form.CloseHandle), factory.Ap_UI_Form_CloseHandle);
		dict.Add(typeof(Ap.UI.Form.FocusChangedHandle), factory.Ap_UI_Form_FocusChangedHandle);

		DelegateTraits<System.Action>.Init(factory.System_Action);
		DelegateTraits<UnityEngine.Events.UnityAction>.Init(factory.UnityEngine_Events_UnityAction);
		DelegateTraits<System.Predicate<int>>.Init(factory.System_Predicate_int);
		DelegateTraits<System.Action<int>>.Init(factory.System_Action_int);
		DelegateTraits<System.Comparison<int>>.Init(factory.System_Comparison_int);
		DelegateTraits<System.Func<int,int>>.Init(factory.System_Func_int_int);
		DelegateTraits<UnityEngine.Camera.CameraCallback>.Init(factory.UnityEngine_Camera_CameraCallback);
		DelegateTraits<UnityEngine.Application.LowMemoryCallback>.Init(factory.UnityEngine_Application_LowMemoryCallback);
		DelegateTraits<UnityEngine.Application.AdvertisingIdentifierCallback>.Init(factory.UnityEngine_Application_AdvertisingIdentifierCallback);
		DelegateTraits<UnityEngine.Application.LogCallback>.Init(factory.UnityEngine_Application_LogCallback);
		DelegateTraits<UnityEngine.AudioClip.PCMReaderCallback>.Init(factory.UnityEngine_AudioClip_PCMReaderCallback);
		DelegateTraits<UnityEngine.AudioClip.PCMSetPositionCallback>.Init(factory.UnityEngine_AudioClip_PCMSetPositionCallback);
		DelegateTraits<Ap.UI.Control.LoadAssetStartHandle>.Init(factory.Ap_UI_Control_LoadAssetStartHandle);
		DelegateTraits<Ap.UI.Control.LoadAssetEndHandle>.Init(factory.Ap_UI_Control_LoadAssetEndHandle);
		DelegateTraits<Ap.UI.Form.LoadHandle>.Init(factory.Ap_UI_Form_LoadHandle);
		DelegateTraits<Ap.UI.Form.CloseHandle>.Init(factory.Ap_UI_Form_CloseHandle);
		DelegateTraits<Ap.UI.Form.FocusChangedHandle>.Init(factory.Ap_UI_Form_FocusChangedHandle);

		TypeTraits<System.Action>.Init(factory.Check_System_Action);
		TypeTraits<UnityEngine.Events.UnityAction>.Init(factory.Check_UnityEngine_Events_UnityAction);
		TypeTraits<System.Predicate<int>>.Init(factory.Check_System_Predicate_int);
		TypeTraits<System.Action<int>>.Init(factory.Check_System_Action_int);
		TypeTraits<System.Comparison<int>>.Init(factory.Check_System_Comparison_int);
		TypeTraits<System.Func<int,int>>.Init(factory.Check_System_Func_int_int);
		TypeTraits<UnityEngine.Camera.CameraCallback>.Init(factory.Check_UnityEngine_Camera_CameraCallback);
		TypeTraits<UnityEngine.Application.LowMemoryCallback>.Init(factory.Check_UnityEngine_Application_LowMemoryCallback);
		TypeTraits<UnityEngine.Application.AdvertisingIdentifierCallback>.Init(factory.Check_UnityEngine_Application_AdvertisingIdentifierCallback);
		TypeTraits<UnityEngine.Application.LogCallback>.Init(factory.Check_UnityEngine_Application_LogCallback);
		TypeTraits<UnityEngine.AudioClip.PCMReaderCallback>.Init(factory.Check_UnityEngine_AudioClip_PCMReaderCallback);
		TypeTraits<UnityEngine.AudioClip.PCMSetPositionCallback>.Init(factory.Check_UnityEngine_AudioClip_PCMSetPositionCallback);
		TypeTraits<Ap.UI.Control.LoadAssetStartHandle>.Init(factory.Check_Ap_UI_Control_LoadAssetStartHandle);
		TypeTraits<Ap.UI.Control.LoadAssetEndHandle>.Init(factory.Check_Ap_UI_Control_LoadAssetEndHandle);
		TypeTraits<Ap.UI.Form.LoadHandle>.Init(factory.Check_Ap_UI_Form_LoadHandle);
		TypeTraits<Ap.UI.Form.CloseHandle>.Init(factory.Check_Ap_UI_Form_CloseHandle);
		TypeTraits<Ap.UI.Form.FocusChangedHandle>.Init(factory.Check_Ap_UI_Form_FocusChangedHandle);

		StackTraits<System.Action>.Push = factory.Push_System_Action;
		StackTraits<UnityEngine.Events.UnityAction>.Push = factory.Push_UnityEngine_Events_UnityAction;
		StackTraits<System.Predicate<int>>.Push = factory.Push_System_Predicate_int;
		StackTraits<System.Action<int>>.Push = factory.Push_System_Action_int;
		StackTraits<System.Comparison<int>>.Push = factory.Push_System_Comparison_int;
		StackTraits<System.Func<int,int>>.Push = factory.Push_System_Func_int_int;
		StackTraits<UnityEngine.Camera.CameraCallback>.Push = factory.Push_UnityEngine_Camera_CameraCallback;
		StackTraits<UnityEngine.Application.LowMemoryCallback>.Push = factory.Push_UnityEngine_Application_LowMemoryCallback;
		StackTraits<UnityEngine.Application.AdvertisingIdentifierCallback>.Push = factory.Push_UnityEngine_Application_AdvertisingIdentifierCallback;
		StackTraits<UnityEngine.Application.LogCallback>.Push = factory.Push_UnityEngine_Application_LogCallback;
		StackTraits<UnityEngine.AudioClip.PCMReaderCallback>.Push = factory.Push_UnityEngine_AudioClip_PCMReaderCallback;
		StackTraits<UnityEngine.AudioClip.PCMSetPositionCallback>.Push = factory.Push_UnityEngine_AudioClip_PCMSetPositionCallback;
		StackTraits<Ap.UI.Control.LoadAssetStartHandle>.Push = factory.Push_Ap_UI_Control_LoadAssetStartHandle;
		StackTraits<Ap.UI.Control.LoadAssetEndHandle>.Push = factory.Push_Ap_UI_Control_LoadAssetEndHandle;
		StackTraits<Ap.UI.Form.LoadHandle>.Push = factory.Push_Ap_UI_Form_LoadHandle;
		StackTraits<Ap.UI.Form.CloseHandle>.Push = factory.Push_Ap_UI_Form_CloseHandle;
		StackTraits<Ap.UI.Form.FocusChangedHandle>.Push = factory.Push_Ap_UI_Form_FocusChangedHandle;
	}
    
    public static Delegate CreateDelegate(Type t, LuaFunction func = null)
    {
        DelegateCreate Create = null;

        if (!dict.TryGetValue(t, out Create))
        {
            throw new LuaException(string.Format("Delegate {0} not register", LuaMisc.GetTypeName(t)));            
        }

        if (func != null)
        {
            LuaState state = func.GetLuaState();
            LuaDelegate target = state.GetLuaDelegate(func);
            
            if (target != null)
            {
                return Delegate.CreateDelegate(t, target, target.method);
            }  
            else
            {
                Delegate d = Create(func, null, false);
                target = d.Target as LuaDelegate;
                state.AddLuaDelegate(target, func);
                return d;
            }       
        }

        return Create(null, null, false);        
    }
    
    public static Delegate CreateDelegate(Type t, LuaFunction func, LuaTable self)
    {
        DelegateCreate Create = null;

        if (!dict.TryGetValue(t, out Create))
        {
            throw new LuaException(string.Format("Delegate {0} not register", LuaMisc.GetTypeName(t)));
        }

        if (func != null)
        {
            LuaState state = func.GetLuaState();
            LuaDelegate target = state.GetLuaDelegate(func, self);

            if (target != null)
            {
                return Delegate.CreateDelegate(t, target, target.method);
            }
            else
            {
                Delegate d = Create(func, self, true);
                target = d.Target as LuaDelegate;
                state.AddLuaDelegate(target, func, self);
                return d;
            }
        }

        return Create(null, null, true);
    }
    
    public static Delegate RemoveDelegate(Delegate obj, LuaFunction func)
    {
        LuaState state = func.GetLuaState();
        Delegate[] ds = obj.GetInvocationList();

        for (int i = 0; i < ds.Length; i++)
        {
            LuaDelegate ld = ds[i].Target as LuaDelegate;

            if (ld != null && ld.func == func)
            {
                obj = Delegate.Remove(obj, ds[i]);
                state.DelayDispose(ld.func);
                break;
            }
        }

        return obj;
    }
    
    public static Delegate RemoveDelegate(Delegate obj, Delegate dg)
    {
        LuaDelegate remove = dg.Target as LuaDelegate;

        if (remove == null)
        {
            obj = Delegate.Remove(obj, dg);
            return obj;
        }

        LuaState state = remove.func.GetLuaState();
        Delegate[] ds = obj.GetInvocationList();        

        for (int i = 0; i < ds.Length; i++)
        {
            LuaDelegate ld = ds[i].Target as LuaDelegate;

            if (ld != null && ld == remove)
            {
                obj = Delegate.Remove(obj, ds[i]);
                state.DelayDispose(ld.func);
                state.DelayDispose(ld.self);
                break;
            }
        }

        return obj;
    }

	class System_Action_Event : LuaDelegate
	{
		public System_Action_Event(LuaFunction func) : base(func) { }
		public System_Action_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call()
		{
			func.Call();
		}

		public void CallWithSelf()
		{
			func.BeginPCall();
			func.Push(self);
			func.PCall();
			func.EndPCall();
		}
	}

	public System.Action System_Action(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			System.Action fn = delegate() { };
			return fn;
		}

		if(!flag)
		{
			System_Action_Event target = new System_Action_Event(func);
			System.Action d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			System_Action_Event target = new System_Action_Event(func, self);
			System.Action d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_System_Action(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(System.Action), L, pos);
	}

	void Push_System_Action(IntPtr L, System.Action o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_Events_UnityAction_Event : LuaDelegate
	{
		public UnityEngine_Events_UnityAction_Event(LuaFunction func) : base(func) { }
		public UnityEngine_Events_UnityAction_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call()
		{
			func.Call();
		}

		public void CallWithSelf()
		{
			func.BeginPCall();
			func.Push(self);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.Events.UnityAction UnityEngine_Events_UnityAction(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.Events.UnityAction fn = delegate() { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_Events_UnityAction_Event target = new UnityEngine_Events_UnityAction_Event(func);
			UnityEngine.Events.UnityAction d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_Events_UnityAction_Event target = new UnityEngine_Events_UnityAction_Event(func, self);
			UnityEngine.Events.UnityAction d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_Events_UnityAction(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.Events.UnityAction), L, pos);
	}

	void Push_UnityEngine_Events_UnityAction(IntPtr L, UnityEngine.Events.UnityAction o)
	{
		ToLua.Push(L, o);
	}

	class System_Predicate_int_Event : LuaDelegate
	{
		public System_Predicate_int_Event(LuaFunction func) : base(func) { }
		public System_Predicate_int_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public bool Call(int param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			bool ret = func.CheckBoolean();
			func.EndPCall();
			return ret;
		}

		public bool CallWithSelf(int param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.PCall();
			bool ret = func.CheckBoolean();
			func.EndPCall();
			return ret;
		}
	}

	public System.Predicate<int> System_Predicate_int(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			System.Predicate<int> fn = delegate(int param0) { return false; };
			return fn;
		}

		if(!flag)
		{
			System_Predicate_int_Event target = new System_Predicate_int_Event(func);
			System.Predicate<int> d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			System_Predicate_int_Event target = new System_Predicate_int_Event(func, self);
			System.Predicate<int> d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_System_Predicate_int(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(System.Predicate<int>), L, pos);
	}

	void Push_System_Predicate_int(IntPtr L, System.Predicate<int> o)
	{
		ToLua.Push(L, o);
	}

	class System_Action_int_Event : LuaDelegate
	{
		public System_Action_int_Event(LuaFunction func) : base(func) { }
		public System_Action_int_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(int param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(int param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public System.Action<int> System_Action_int(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			System.Action<int> fn = delegate(int param0) { };
			return fn;
		}

		if(!flag)
		{
			System_Action_int_Event target = new System_Action_int_Event(func);
			System.Action<int> d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			System_Action_int_Event target = new System_Action_int_Event(func, self);
			System.Action<int> d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_System_Action_int(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(System.Action<int>), L, pos);
	}

	void Push_System_Action_int(IntPtr L, System.Action<int> o)
	{
		ToLua.Push(L, o);
	}

	class System_Comparison_int_Event : LuaDelegate
	{
		public System_Comparison_int_Event(LuaFunction func) : base(func) { }
		public System_Comparison_int_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public int Call(int param0, int param1)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			int ret = (int)func.CheckNumber();
			func.EndPCall();
			return ret;
		}

		public int CallWithSelf(int param0, int param1)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			int ret = (int)func.CheckNumber();
			func.EndPCall();
			return ret;
		}
	}

	public System.Comparison<int> System_Comparison_int(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			System.Comparison<int> fn = delegate(int param0, int param1) { return 0; };
			return fn;
		}

		if(!flag)
		{
			System_Comparison_int_Event target = new System_Comparison_int_Event(func);
			System.Comparison<int> d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			System_Comparison_int_Event target = new System_Comparison_int_Event(func, self);
			System.Comparison<int> d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_System_Comparison_int(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(System.Comparison<int>), L, pos);
	}

	void Push_System_Comparison_int(IntPtr L, System.Comparison<int> o)
	{
		ToLua.Push(L, o);
	}

	class System_Func_int_int_Event : LuaDelegate
	{
		public System_Func_int_int_Event(LuaFunction func) : base(func) { }
		public System_Func_int_int_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public int Call(int param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			int ret = (int)func.CheckNumber();
			func.EndPCall();
			return ret;
		}

		public int CallWithSelf(int param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.PCall();
			int ret = (int)func.CheckNumber();
			func.EndPCall();
			return ret;
		}
	}

	public System.Func<int,int> System_Func_int_int(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			System.Func<int,int> fn = delegate(int param0) { return 0; };
			return fn;
		}

		if(!flag)
		{
			System_Func_int_int_Event target = new System_Func_int_int_Event(func);
			System.Func<int,int> d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			System_Func_int_int_Event target = new System_Func_int_int_Event(func, self);
			System.Func<int,int> d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_System_Func_int_int(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(System.Func<int,int>), L, pos);
	}

	void Push_System_Func_int_int(IntPtr L, System.Func<int,int> o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_Camera_CameraCallback_Event : LuaDelegate
	{
		public UnityEngine_Camera_CameraCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_Camera_CameraCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(UnityEngine.Camera param0)
		{
			func.BeginPCall();
			func.PushSealed(param0);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(UnityEngine.Camera param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.PushSealed(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.Camera.CameraCallback UnityEngine_Camera_CameraCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.Camera.CameraCallback fn = delegate(UnityEngine.Camera param0) { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_Camera_CameraCallback_Event target = new UnityEngine_Camera_CameraCallback_Event(func);
			UnityEngine.Camera.CameraCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_Camera_CameraCallback_Event target = new UnityEngine_Camera_CameraCallback_Event(func, self);
			UnityEngine.Camera.CameraCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_Camera_CameraCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.Camera.CameraCallback), L, pos);
	}

	void Push_UnityEngine_Camera_CameraCallback(IntPtr L, UnityEngine.Camera.CameraCallback o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_Application_LowMemoryCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_LowMemoryCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_Application_LowMemoryCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call()
		{
			func.Call();
		}

		public void CallWithSelf()
		{
			func.BeginPCall();
			func.Push(self);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.Application.LowMemoryCallback UnityEngine_Application_LowMemoryCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.Application.LowMemoryCallback fn = delegate() { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_Application_LowMemoryCallback_Event target = new UnityEngine_Application_LowMemoryCallback_Event(func);
			UnityEngine.Application.LowMemoryCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_Application_LowMemoryCallback_Event target = new UnityEngine_Application_LowMemoryCallback_Event(func, self);
			UnityEngine.Application.LowMemoryCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_Application_LowMemoryCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.Application.LowMemoryCallback), L, pos);
	}

	void Push_UnityEngine_Application_LowMemoryCallback(IntPtr L, UnityEngine.Application.LowMemoryCallback o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_Application_AdvertisingIdentifierCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_AdvertisingIdentifierCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_Application_AdvertisingIdentifierCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(string param0, bool param1, string param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(string param0, bool param1, string param2)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.Application.AdvertisingIdentifierCallback UnityEngine_Application_AdvertisingIdentifierCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.Application.AdvertisingIdentifierCallback fn = delegate(string param0, bool param1, string param2) { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_Application_AdvertisingIdentifierCallback_Event target = new UnityEngine_Application_AdvertisingIdentifierCallback_Event(func);
			UnityEngine.Application.AdvertisingIdentifierCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_Application_AdvertisingIdentifierCallback_Event target = new UnityEngine_Application_AdvertisingIdentifierCallback_Event(func, self);
			UnityEngine.Application.AdvertisingIdentifierCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_Application_AdvertisingIdentifierCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.Application.AdvertisingIdentifierCallback), L, pos);
	}

	void Push_UnityEngine_Application_AdvertisingIdentifierCallback(IntPtr L, UnityEngine.Application.AdvertisingIdentifierCallback o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_Application_LogCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_LogCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_Application_LogCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(string param0, string param1, UnityEngine.LogType param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(string param0, string param1, UnityEngine.LogType param2)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.Application.LogCallback UnityEngine_Application_LogCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.Application.LogCallback fn = delegate(string param0, string param1, UnityEngine.LogType param2) { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_Application_LogCallback_Event target = new UnityEngine_Application_LogCallback_Event(func);
			UnityEngine.Application.LogCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_Application_LogCallback_Event target = new UnityEngine_Application_LogCallback_Event(func, self);
			UnityEngine.Application.LogCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_Application_LogCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.Application.LogCallback), L, pos);
	}

	void Push_UnityEngine_Application_LogCallback(IntPtr L, UnityEngine.Application.LogCallback o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_AudioClip_PCMReaderCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMReaderCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_AudioClip_PCMReaderCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(float[] param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(float[] param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.AudioClip.PCMReaderCallback UnityEngine_AudioClip_PCMReaderCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.AudioClip.PCMReaderCallback fn = delegate(float[] param0) { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_AudioClip_PCMReaderCallback_Event target = new UnityEngine_AudioClip_PCMReaderCallback_Event(func);
			UnityEngine.AudioClip.PCMReaderCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_AudioClip_PCMReaderCallback_Event target = new UnityEngine_AudioClip_PCMReaderCallback_Event(func, self);
			UnityEngine.AudioClip.PCMReaderCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_AudioClip_PCMReaderCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.AudioClip.PCMReaderCallback), L, pos);
	}

	void Push_UnityEngine_AudioClip_PCMReaderCallback(IntPtr L, UnityEngine.AudioClip.PCMReaderCallback o)
	{
		ToLua.Push(L, o);
	}

	class UnityEngine_AudioClip_PCMSetPositionCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMSetPositionCallback_Event(LuaFunction func) : base(func) { }
		public UnityEngine_AudioClip_PCMSetPositionCallback_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(int param0)
		{
			func.BeginPCall();
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(int param0)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.PCall();
			func.EndPCall();
		}
	}

	public UnityEngine.AudioClip.PCMSetPositionCallback UnityEngine_AudioClip_PCMSetPositionCallback(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			UnityEngine.AudioClip.PCMSetPositionCallback fn = delegate(int param0) { };
			return fn;
		}

		if(!flag)
		{
			UnityEngine_AudioClip_PCMSetPositionCallback_Event target = new UnityEngine_AudioClip_PCMSetPositionCallback_Event(func);
			UnityEngine.AudioClip.PCMSetPositionCallback d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			UnityEngine_AudioClip_PCMSetPositionCallback_Event target = new UnityEngine_AudioClip_PCMSetPositionCallback_Event(func, self);
			UnityEngine.AudioClip.PCMSetPositionCallback d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_UnityEngine_AudioClip_PCMSetPositionCallback(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(UnityEngine.AudioClip.PCMSetPositionCallback), L, pos);
	}

	void Push_UnityEngine_AudioClip_PCMSetPositionCallback(IntPtr L, UnityEngine.AudioClip.PCMSetPositionCallback o)
	{
		ToLua.Push(L, o);
	}

	class Ap_UI_Control_LoadAssetStartHandle_Event : LuaDelegate
	{
		public Ap_UI_Control_LoadAssetStartHandle_Event(LuaFunction func) : base(func) { }
		public Ap_UI_Control_LoadAssetStartHandle_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(object param0, int param1, string param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(object param0, int param1, string param2)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public Ap.UI.Control.LoadAssetStartHandle Ap_UI_Control_LoadAssetStartHandle(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			Ap.UI.Control.LoadAssetStartHandle fn = delegate(object param0, int param1, string param2) { };
			return fn;
		}

		if(!flag)
		{
			Ap_UI_Control_LoadAssetStartHandle_Event target = new Ap_UI_Control_LoadAssetStartHandle_Event(func);
			Ap.UI.Control.LoadAssetStartHandle d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			Ap_UI_Control_LoadAssetStartHandle_Event target = new Ap_UI_Control_LoadAssetStartHandle_Event(func, self);
			Ap.UI.Control.LoadAssetStartHandle d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_Ap_UI_Control_LoadAssetStartHandle(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(Ap.UI.Control.LoadAssetStartHandle), L, pos);
	}

	void Push_Ap_UI_Control_LoadAssetStartHandle(IntPtr L, Ap.UI.Control.LoadAssetStartHandle o)
	{
		ToLua.Push(L, o);
	}

	class Ap_UI_Control_LoadAssetEndHandle_Event : LuaDelegate
	{
		public Ap_UI_Control_LoadAssetEndHandle_Event(LuaFunction func) : base(func) { }
		public Ap_UI_Control_LoadAssetEndHandle_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(object param0, int param1, string param2, UnityEngine.Object param3)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.Push(param3);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(object param0, int param1, string param2, UnityEngine.Object param3)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.Push(param3);
			func.PCall();
			func.EndPCall();
		}
	}

	public Ap.UI.Control.LoadAssetEndHandle Ap_UI_Control_LoadAssetEndHandle(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			Ap.UI.Control.LoadAssetEndHandle fn = delegate(object param0, int param1, string param2, UnityEngine.Object param3) { };
			return fn;
		}

		if(!flag)
		{
			Ap_UI_Control_LoadAssetEndHandle_Event target = new Ap_UI_Control_LoadAssetEndHandle_Event(func);
			Ap.UI.Control.LoadAssetEndHandle d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			Ap_UI_Control_LoadAssetEndHandle_Event target = new Ap_UI_Control_LoadAssetEndHandle_Event(func, self);
			Ap.UI.Control.LoadAssetEndHandle d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_Ap_UI_Control_LoadAssetEndHandle(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(Ap.UI.Control.LoadAssetEndHandle), L, pos);
	}

	void Push_Ap_UI_Control_LoadAssetEndHandle(IntPtr L, Ap.UI.Control.LoadAssetEndHandle o)
	{
		ToLua.Push(L, o);
	}

	class Ap_UI_Form_LoadHandle_Event : LuaDelegate
	{
		public Ap_UI_Form_LoadHandle_Event(LuaFunction func) : base(func) { }
		public Ap_UI_Form_LoadHandle_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(object param0, int param1)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(object param0, int param1)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			func.EndPCall();
		}
	}

	public Ap.UI.Form.LoadHandle Ap_UI_Form_LoadHandle(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			Ap.UI.Form.LoadHandle fn = delegate(object param0, int param1) { };
			return fn;
		}

		if(!flag)
		{
			Ap_UI_Form_LoadHandle_Event target = new Ap_UI_Form_LoadHandle_Event(func);
			Ap.UI.Form.LoadHandle d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			Ap_UI_Form_LoadHandle_Event target = new Ap_UI_Form_LoadHandle_Event(func, self);
			Ap.UI.Form.LoadHandle d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_Ap_UI_Form_LoadHandle(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(Ap.UI.Form.LoadHandle), L, pos);
	}

	void Push_Ap_UI_Form_LoadHandle(IntPtr L, Ap.UI.Form.LoadHandle o)
	{
		ToLua.Push(L, o);
	}

	class Ap_UI_Form_CloseHandle_Event : LuaDelegate
	{
		public Ap_UI_Form_CloseHandle_Event(LuaFunction func) : base(func) { }
		public Ap_UI_Form_CloseHandle_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(object param0, int param1)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(object param0, int param1)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.PCall();
			func.EndPCall();
		}
	}

	public Ap.UI.Form.CloseHandle Ap_UI_Form_CloseHandle(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			Ap.UI.Form.CloseHandle fn = delegate(object param0, int param1) { };
			return fn;
		}

		if(!flag)
		{
			Ap_UI_Form_CloseHandle_Event target = new Ap_UI_Form_CloseHandle_Event(func);
			Ap.UI.Form.CloseHandle d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			Ap_UI_Form_CloseHandle_Event target = new Ap_UI_Form_CloseHandle_Event(func, self);
			Ap.UI.Form.CloseHandle d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_Ap_UI_Form_CloseHandle(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(Ap.UI.Form.CloseHandle), L, pos);
	}

	void Push_Ap_UI_Form_CloseHandle(IntPtr L, Ap.UI.Form.CloseHandle o)
	{
		ToLua.Push(L, o);
	}

	class Ap_UI_Form_FocusChangedHandle_Event : LuaDelegate
	{
		public Ap_UI_Form_FocusChangedHandle_Event(LuaFunction func) : base(func) { }
		public Ap_UI_Form_FocusChangedHandle_Event(LuaFunction func, LuaTable self) : base(func, self) { }

		public void Call(object param0, int param1, bool param2)
		{
			func.BeginPCall();
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}

		public void CallWithSelf(object param0, int param1, bool param2)
		{
			func.BeginPCall();
			func.Push(self);
			func.Push(param0);
			func.Push(param1);
			func.Push(param2);
			func.PCall();
			func.EndPCall();
		}
	}

	public Ap.UI.Form.FocusChangedHandle Ap_UI_Form_FocusChangedHandle(LuaFunction func, LuaTable self, bool flag)
	{
		if (func == null)
		{
			Ap.UI.Form.FocusChangedHandle fn = delegate(object param0, int param1, bool param2) { };
			return fn;
		}

		if(!flag)
		{
			Ap_UI_Form_FocusChangedHandle_Event target = new Ap_UI_Form_FocusChangedHandle_Event(func);
			Ap.UI.Form.FocusChangedHandle d = target.Call;
			target.method = d.Method;
			return d;
		}
		else
		{
			Ap_UI_Form_FocusChangedHandle_Event target = new Ap_UI_Form_FocusChangedHandle_Event(func, self);
			Ap.UI.Form.FocusChangedHandle d = target.CallWithSelf;
			target.method = d.Method;
			return d;
		}
	}

	bool Check_Ap_UI_Form_FocusChangedHandle(IntPtr L, int pos)
	{
		return TypeChecker.CheckDelegateType(typeof(Ap.UI.Form.FocusChangedHandle), L, pos);
	}

	void Push_Ap_UI_Form_FocusChangedHandle(IntPtr L, Ap.UI.Form.FocusChangedHandle o)
	{
		ToLua.Push(L, o);
	}

}

