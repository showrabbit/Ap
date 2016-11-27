using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Ap.UI
{
    public class ScrollRectEx : ScrollRect
    {
        /// <summary>
        /// 控件特殊标签
        /// </summary>
        public object CtrTag
        {
            get
            {
                return m_CtrTag;
            }
            set
            {
                m_CtrTag = value;
            }
        }
        protected object m_CtrTag = "";

        /// <summary>
        /// 控件的id
        /// </summary>
        public int ID
        {
            set
            {
                m_ID = value;
            }
            get
            {
                return m_ID;
            }
        }
        protected int m_ID = 0;
    }

}

