using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ap.UI.Depth
{
    /// <summary>
    /// 深度对象
    /// </summary>
    public class UIDepthBase : MonoBehaviour, IUIDepth
    {
        /// <summary>
        /// 深度类型
        /// </summary>
        public DepthType Type
        {
            set;
            get;
        }

        /// <summary>
        /// 深度值
        /// </summary>
        protected int m_Depth;


        public void Awake()
        {
            
        }


        public virtual int GetDepth()
        {
            return m_Depth;
        }

        public virtual void SetDepth(int depth)
        {
            m_Depth = depth;

        }
    }
}
