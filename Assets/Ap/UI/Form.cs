using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Base;
using UnityEngine;
using Ap.Managers;
using Ap.UI.Interface;

namespace Ap.UI
{
    public class Form : Control, IView
    {
        /// <summary>
        /// 显示层级的索引
        /// </summary>
        public int LayerIndex = 0;

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
        public bool IsFull = true;

        protected List<SortingSetter> m_Sorting = new List<SortingSetter>();

        public void Start()
        {
            SortingSetter curr = this.GetComponent<SortingSetter>();
            if (curr != null)
            {
                m_Sorting.Add(curr);
            }
            SortingSetter[] child = this.GetComponentsInChildren<SortingSetter>();
            if (child != null && child.Length > 0)
                m_Sorting.AddRange(child);
        }

        /// <summary>
        /// 加载中
        /// </summary>
        public virtual void OnLoad()
        {
            for (int i = 0; i < m_Sorting.Count; i++)
            {
                m_Sorting[i].Sort();
            }

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
