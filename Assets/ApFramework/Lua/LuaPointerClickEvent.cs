using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.UI;

namespace Ap.Lua
{
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    public class LuaPointerClickEvent : LuaControlEvent
    {
        public override LuaInterface.LuaFunction Fun
        {
            get
            {
                return base.Fun;
            }
            set
            {
                base.Fun = value;
                m_Sender.onClick.AddListener(delegate() { m_Fun.Call(m_Sender); });
            }
        }

        private ButtonEx m_Sender;

        public LuaPointerClickEvent(ButtonEx sender)
        {
            m_Sender = sender;
        }

        public static LuaPointerClickEvent Create(ButtonEx sender)
        {
            return new LuaPointerClickEvent(sender);
        }
    }
}
