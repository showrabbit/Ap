using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Base;
using Ap.UI;
using System.Collections;
using UnityEngine;

namespace Ap.Managers
{
    public class FormManager : ManagerBase<FormManager>
    {
        protected Dictionary<int, Form> m_Forms = new Dictionary<int, Form>();
        protected int m_IdIndex = 0;
        protected GameObject m_FormRoot;
        protected Transform m_FormRootTrans;

        protected override void Init()
        {
            m_IdIndex = 0;
            m_FormRoot = GameObject.Find("FormRoot");
            m_FormRootTrans = m_FormRoot.transform;
        }
        /// <summary>
        /// 非模态打开 - 全屏模式
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public int Show(string formName)
        {
            m_IdIndex++;
            StartCoroutine(ShowAsync(formName));
            return m_IdIndex;
        }

        /// <summary>
        /// 模态打开 - 非全屏模式
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public int ShowDialog(string formName)
        {
            return 0;
        }
        protected IEnumerator ShowAsync(string formName)
        {

            AssetBundleLoadAssetOperation ab = AssetBundleManager.Instance.LoadAssetAsync(formName, typeof(GameObject));
            yield return ab;

            Form form = ab.GetAsset<GameObject>().GetComponent<Form>();
            m_Forms.Add(m_IdIndex, form);
            form.ID = m_IdIndex;

            // -----------绑定事件

            form.Load += FormLoad;
            form.Close += FormClose;
            form.LoadAssetStart += FormLoadAssetStart;
            form.LoadAssetEnd += FormLoadAssetEnd;

            // --------------------

            form.OnLoad();
            form.Focus = true;

        }
        public int Close(int id)
        {
            if (m_Forms.ContainsKey(id))
            {
                m_Forms[id].OnClose();
                m_Forms.Remove(id);
            }
            return 0;
        }

        

        /// <summary>
        /// 窗体加载中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        protected void FormLoad(object sender, int id)
        {
            LuaManager.Instance.CallFunction("FormManager.FormLoad", id);
        }
        protected void FormClose(object sender,int id)
        {
            LuaManager.Instance.CallFunction("FormManager.FormClose", id);
        }

        protected void FormLoadAssetStart(object sender, int id, string assetName)
        {
            LuaManager.Instance.CallFunction("FormManager.FormLoadAssetStart", id, assetName);
        }

        protected void FormLoadAssetEnd(object sender, int id, string assetName, UnityEngine.Object obj)
        {
            LuaManager.Instance.CallFunction("FormManager.FormLoadAssetEnd", id, assetName, obj);
        }
    }
}
