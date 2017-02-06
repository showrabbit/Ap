using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Base;
using UnityEngine;
using Ap.Managers;

namespace Ap.UI
{
    public class Form : Control, IView
    {

        public delegate void LoadHandle(object sender, int id);
        public delegate void CloseHandle(object sender, int id);
        public delegate void FocusChangedHandle(object sender, int id, bool focus);

        public LoadHandle Load;
        public CloseHandle Close;
        public FocusChangedHandle FocusChanged;
        /// <summary>
        /// 是否可见
        /// </summary>
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public virtual bool Focus
        {
            set
            {
                m_Focus = value;

                if (FocusChanged != null)
                {
                    FocusChanged(this, this.ID, value);
                }
            }
            get
            {
                return m_Focus;
            }
        }
        protected bool m_Focus = false;
        /// <summary>
        /// 是否是全屏界面
        /// </summary>
        public bool IsFull
        {
            set
            {
                m_IsFull = value;
            }
            get
            {
                return m_IsFull;
            }
        }
        protected bool m_IsFull = true;

        /// <summary>
        /// 加载中
        /// </summary>
        public virtual void OnLoad()
        {
            if (Load != null)
            {
                Load(this, m_ID);
            }
        }

        /// <summary>
        /// 关闭,这个界面彻底会被删除
        /// </summary>
        public virtual void OnClose()
        {
            GameObject.Destroy(this.gameObject);
            if (Close != null)
            {
                Close(this, m_ID);
            }
        }

        protected override void OnLoadAssetStart(string assetName)
        {
            base.OnLoadAssetStart(assetName);
        }

        protected override void OnLoadAssetEnd(string assetName, UnityEngine.Object obj)
        {
            base.OnLoadAssetEnd(assetName, obj);
        }

        public void Changed(string type, params object[] values)
        {

        }
    }
}
