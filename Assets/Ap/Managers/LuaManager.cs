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

        public void Awake()
        {
            // 这里会添加Tolua的目录
            m_Lua = new LuaState();
            OpenLibs();
            m_Lua.LuaSetTop(0);
            LuaBinder.Bind(m_Lua);
            // 增加项目本身的lua目录
            // tolua的文件路径已经在LuaState里面添加了
            // 添加项目的脚本路径
            m_Lua.AddSearchPath(Environment.LuaPath + "/Lua");
            m_Lua.Start();    //启动LUAVM
        }

        protected override void Init()
        {

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

        public void DoFile(string filename)
        {
            m_Lua.DoFile(filename);
        }

        // Update is called once per frame
        public void CallFunction(string funcName, params object[] args)
        {
            LuaFunction func = m_Lua.GetFunction(funcName);
            if (func != null)
            {
                if (args == null || args.Length == 0)
                {
                    func.Call();
                }
                else
                {
                    if (args.Length == 1)
                    {
                        func.Call(args[0]);
                    }
                    else if (args.Length == 2)
                    {
                        func.Call(args[0], args[1]);
                    }
                    else if (args.Length == 3)
                    {
                        func.Call(args[0], args[1], args[2]);
                    }
                    else if (args.Length == 4)
                    {
                        func.Call(args[0], args[1], args[2], args[3]);
                    }
                    else if (args.Length == 5)
                    {
                        func.Call(args[0], args[1], args[2], args[3], args[4]);
                    }
                    else if (args.Length == 6)
                    {
                        func.Call(args[0], args[1], args[2], args[3], args[4], args[5]);
                    }
                    else if (args.Length == 7)
                    {
                        func.Call(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                    }
                    else if (args.Length == 8)
                    {
                        func.Call(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                    }
                    else if (args.Length == 9)
                    {
                        func.Call(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                    }
                    else
                        func.Call<object[]>(args);
                }
            }
            return;
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