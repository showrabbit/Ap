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
        /// <summary>
        /// 窗体集合
        /// </summary>
        //protected Dictionary<int, FormCtr> m_FormCtrs = new Dictionary<int, FormCtr>();

        protected Dictionary<int, Form> m_Forms = new Dictionary<int, Form>();
        protected Dictionary<int, IController[]> m_FormCtrs = new Dictionary<int, IController[]>();
        /// <summary>
        /// ID索引
        /// </summary>
        protected int m_IdIndex = 0;
        /// <summary>
        /// 窗体根目录
        /// </summary>
        protected GameObject m_FormRoot;
        /// <summary>
        /// 窗体根目录
        /// </summary>
        protected Transform m_FormRootTrans;

        public void Awake()
        {
            m_IdIndex = 0;
            m_FormRoot = GameObject.Find("FormRoot");
            if (m_FormRoot == null)
            {
                Ap.Tools.Logger.Instance.Write("FormManager Init Error");
                return;
            }
            else
                m_FormRootTrans = m_FormRoot.transform;
        }

        protected override void Init()
        {

        }
        /// <summary>
        /// 非模态打开 - 全屏模式
        /// 全屏模式会自动隐藏背景界面
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public int Show(string formName)
        {
            m_IdIndex++;
            StartCoroutine(ShowAsync(formName));
            return m_IdIndex;
        }

        protected IEnumerator ShowAsync(string formName)
        {

            AssetBundleLoadAssetOperation ab = AssetBundleManager.Instance.LoadAssetAsync(formName, typeof(GameObject));
            yield return ab;
            GameObject obj = GameObject.Instantiate(ab.GetAsset<GameObject>());
            RectTransform reTrans = obj.GetComponent<RectTransform>();
            reTrans.SetParent(m_FormRootTrans);
            // 确保界面整个沾满
            reTrans.localScale = Vector3.one;
            reTrans.localPosition = Vector3.zero;
            reTrans.anchorMin = Vector2.zero;
            reTrans.anchorMax = Vector2.one;
            reTrans.offsetMin = Vector2.zero;
            reTrans.offsetMax = Vector2.zero;

            Form form = obj.GetComponent<Form>();
            m_Forms.Add(m_IdIndex, form);
            form.ID = m_IdIndex;

            // 绑定控制器
            IController[] ctrs = form.gameObject.GetComponents<IController>();
            if (ctrs != null)
            {
                for (int i = 0; i < ctrs.Length; i++)
                {
                    ctrs[i].BindView(form);
                }
                m_FormCtrs.Add(m_IdIndex, ctrs);
            }

            // 绑定事件
            form.Load += FormLoad;
            form.Close += FormClose;
            form.LoadAssetStart += FormLoadAssetStart;
            form.LoadAssetEnd += FormLoadAssetEnd;
            form.FocusChanged += FormFocusChanged;

            // --------------------

            form.OnLoad();
            form.Focus = true;

            // todo 处理全屏模式

            yield return null;

        }
        public int Close(int id)
        {
            if (m_Forms.ContainsKey(id))
            {
                m_FormCtrs.Remove(id);
                m_Forms[id].OnClose();
                m_Forms.Remove(id);
                //m_Forms.Remove(id);
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
            LuaManager.Instance.CallFunction("FormManager.FormLoad", id, m_Forms[id]);
        }
        protected void FormClose(object sender, int id)
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
        protected void FormFocusChanged(object sender, int id, bool focus)
        {
            LuaManager.Instance.CallFunction("FormManager.FormFouce", id, focus);
        }
    }
}
