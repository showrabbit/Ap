using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Core;
using Ap.Res;
using UnityEngine;
namespace Ap.UI.Core
{
    public class Control : AssetLoader
    {
        public delegate void LoadAssetStartHandle(object sender, int id, string assetName);
        public delegate void LoadAssetEndHandle(object sender, int id, string assetName, UnityEngine.Object obj);

        public LoadAssetStartHandle LoadAssetStart;
        public LoadAssetEndHandle LoadAssetEnd;

        /// <summary>
        /// 控件包含的子控件
        /// </summary>
        public GameObject[] Controls;

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

        public virtual bool Visible
        {
            set
            {
                m_Visible = value;
                this.gameObject.SetActive(value);
            }
            get
            {
                return m_Visible;
            }
        }
        protected bool m_Visible = true;

        public virtual void LoadPerfab(string assetName)
        {
            LoadAsset<GameObject>(assetName);
        }

        public virtual void LoadSprite(string assetName)
        {
            LoadAsset<Sprite>(assetName);
        }

        protected override void OnLoadAssetStart(string assetName)
        {
            base.OnLoadAssetStart(assetName);
            if (LoadAssetStart != null)
            {
                LoadAssetStart(this, m_ID, assetName);
            }
        }
        protected override void OnLoadAssetEnd(string assetName, UnityEngine.Object obj)
        {
            base.OnLoadAssetEnd(assetName, obj);
            if (LoadAssetEnd != null)
            {
                LoadAssetEnd(this, m_ID, assetName, obj);
            }
        }
    }
}
