using UnityEngine;
using System.Collections;
using LuaInterface;
using Ap.Base;
using Ap.Tools;

namespace Ap.Managers
{
    public class LuaManager : ManagerBase<LuaManager>
    {
        private LuaState m_Lua;

        protected override void Init()
        {
            m_Lua = new LuaState();
            this.OpenLibs();
            m_Lua.LuaSetTop(0);
            LuaBinder.Bind(m_Lua);
            m_Lua.AddSearchPath(Utility.LuaPath);
            this.m_Lua.Start();    //启动LUAVM
            //this.StartMain();
        }


        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson()
        {
            m_Lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            m_Lua.OpenLibs(LuaDLL.luaopen_cjson);
            m_Lua.LuaSetField(-2, "cjson");

            m_Lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            m_Lua.LuaSetField(-2, "cjson.safe");
        }

        public void StartMain()
        {
            m_Lua.DoFile("Main.lua");

            LuaFunction main = m_Lua.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;
        }

        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        void OpenLibs()
        {
            m_Lua.OpenLibs(LuaDLL.luaopen_pb);
            //m_Lua.OpenLibs(LuaDLL.luaopen_sproto_core);
            //m_Lua.OpenLibs(LuaDLL.luaopen_protobuf_c);
            m_Lua.OpenLibs(LuaDLL.luaopen_lpeg);
            m_Lua.OpenLibs(LuaDLL.luaopen_bit);
            m_Lua.OpenLibs(LuaDLL.luaopen_socket_core);

            this.OpenCJson();
        }

        public object[] DoFile(string filename)
        {
            return m_Lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args)
        {
            LuaFunction func = m_Lua.GetFunction(funcName);
            if (func != null)
            {
                return func.Call(args);
            }
            return null;
        }

        public void LuaGC()
        {
            m_Lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public void Close()
        {
            m_Lua.Dispose();
            m_Lua = null;
        }
    }
}