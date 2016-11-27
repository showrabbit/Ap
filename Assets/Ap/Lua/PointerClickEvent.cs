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
    public class PointerClickEvent : ControlEvent
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

        public PointerClickEvent(ButtonEx sender)
        {
            m_Sender = sender;
        }

        public static PointerClickEvent Create(ButtonEx sender)
        {
            return new PointerClickEvent(sender);
        }

        public static PointerClickEvent Create(ButtonEx sender, LuaInterface.LuaFunction fun)
        {
            var l = new PointerClickEvent(sender);
            l.Fun = fun;
            return l;
        }
    }
}
