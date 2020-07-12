using Ap.Core;
using System;
using UnityEngine;

namespace Ap.UI.Core
{
    /// <summary>
    /// Canvas设置器
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class CanvasSorting : SortingSetter
    {
        public enum ModeType
        {
            /// <summary>
            /// 相对模式
            /// </summary>
            Relative,
            /// <summary>
            /// 绝对定位
            /// </summary>
            Absolute
        }

        public ModeType Mode = ModeType.Absolute;

        public int SortingOrder = 0;

        public Canvas Canvas
        {
            get
            {
                return m_Cancas;
            }
        }
        protected Canvas m_Cancas = null;

        public Canvas Parent
        {
            get
            {
                return m_Parent;
            }
        }
        protected Canvas m_Parent = null;

        public void Start()
        {
            m_Cancas = GetComponent<Canvas>();
            m_Parent = GetComponentInParent<Canvas>();
        }

        public override void Sort()
        {
            if (Mode == ModeType.Absolute)
            {
                m_Cancas.sortingOrder = SortingOrder;
            }
            else
            {
                if (m_Parent != null)
                {
                    m_Cancas.sortingOrder = m_Parent.sortingOrder + SortingOrder;
                }
            }

        }
    }
}
