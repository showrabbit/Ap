using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Managers;
using System.Collections;

namespace Ap.UI
{
    public class Form : Control
    {

        public delegate void LoadHandle(object sender, int id);
        public delegate void CloseHandle(object sender, int id);

        public LoadHandle Load;
        public CloseHandle Close;
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
                LuaManager.Instance.CallFunction("FormManager.OnFormFocus", m_ID, value);
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


    }
}
