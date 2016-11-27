using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Base;
using LuaInterface;
namespace Ap.Lua
{
    /// <summary>
    /// c# Lua 控件事件
    /// </summary>
    public class ControlEvent
    {
        public virtual LuaFunction Fun
        {
            set
            {
                m_Fun = value;
            }
            get
            {
                return m_Fun;
            }
        }
        protected LuaFunction m_Fun;


    }
}
