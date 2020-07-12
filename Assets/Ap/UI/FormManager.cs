using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Core;
using Ap.UI;
using System.Collections;
using UnityEngine;
using Ap.Tools;
using Ap.UI.Core;
using Ap.Res;

namespace Ap.UI
{
    public class FormManager : ManagerBase<FormManager>
    {
        /// <summary>
        /// 界面层级
        /// </summary>
        public List<RectTransform> LayerShowedTrans;

        /// <summary>
        /// 用来隐藏的层
        /// </summary>
        public RectTransform LayerHidedTrans;


        /// <summary>
        /// 窗体集合
        /// </summary>
        protected Dictionary<int, Form> m_Forms = new Dictionary<int, Form>();
        protected Dictionary<int, IController[]> m_FormCtrs = new Dictionary<int, IController[]>();
        /// <summary>
        /// 当前显示的界面
        /// </summary>
        public List<Form> m_FormShowed = new List<Form>();
        //当前隐藏的界面
        public System.Collections.Generic.Stack<List<Form>> m_FormHided = new Stack<List<Form>>();
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
        /// <summary>
        /// UI 根节点
        /// </summary>
        public Transform UIRootTrans
        {
            get
            {
                return m_UIRootTrans;
            }
        }
        protected Transform m_UIRootTrans;

        public void Awake()
        {
            m_IdIndex = 0;
            m_UIRootTrans = GameObject.Find("UIRoot").transform;
            m_FormRoot = m_UIRootTrans.Find("Forms").gameObject;
            if (m_FormRoot == null)
            {
                Ap.Tools.Logger.Instance.Write("FormManager Init Error");
                return;
            }
            else
                m_FormRootTrans = m_FormRoot.transform;
            if (m_FormRootTrans.childCount > 0)
            {
                LayerShowedTrans = new List<RectTransform>();
                // 获取层次
                for (int i = 0; i <= m_FormRootTrans.childCount - 1; i++)
                {
                    GameObject child = m_FormRootTrans.GetChild(i).gameObject;
                    RectTransform trans = child.GetComponent<RectTransform>();
                    if(child.name != "Hide")
                    {
                        UIHelper.RectTransformSetFull(trans);
                        LayerShowedTrans.Add(trans);
                    }
                    else
                    {
                        LayerHidedTrans = trans;
                    }
                }
            }
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
            StartCoroutine(ShowAsync(m_IdIndex, formName));
            return m_IdIndex;
        }

        protected IEnumerator ShowAsync(int id, string formName)
        {

            AssetBundleLoadAssetOperation ab = AssetBundleManager.Instance.LoadAssetAsync(formName, typeof(GameObject));
            yield return ab;
            GameObject obj = GameObject.Instantiate(ab.GetAsset<GameObject>());
            AssetBundleManager.Instance.UnloadAssetBundleByAssetName(formName);
            Form form = obj.GetComponent<Form>();
            if (form == null)
            {
                Tools.Logger.Instance.Write("FormManager show error: form null");
                GameObject.Destroy(obj);
                yield break;
            }

            RectTransform reTrans = obj.GetComponent<RectTransform>();
            // 设置层级
            if (this.LayerShowedTrans != null && this.LayerShowedTrans.Count > form.LayerIndex && form.LayerIndex >= 0)
            {
                reTrans.SetParent(this.LayerShowedTrans[form.LayerIndex]);
            }
            else
            {
                Tools.Logger.Instance.Write("FormManager show error: layerindex");
                GameObject.Destroy(obj);
                yield break;
            }

            m_Forms.Add(id, form);
            form.ID = id;

            // 确保界面整个占满
            UIHelper.RectTransformSetFull(reTrans);


            // 绑定控制器
            IController[] ctrs = form.gameObject.GetComponents<IController>();
            if (ctrs != null)
            {
                for (int i = 0; i < ctrs.Length; i++)
                {
                    ctrs[i].BindView(form);
                }
                m_FormCtrs.Add(id, ctrs);
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

            // 处理全屏模式
            if (form.IsFull)
            {
                if (m_FormShowed.Count > 0)
                {
                    List<Form> hided = new List<Form>();
                    for (int i = 0; i < m_FormShowed.Count; i++)
                    {
                        if (m_FormShowed[i].LayerIndex <= form.LayerIndex)
                        {
                            m_FormShowed[i].gameObject.transform.SetParent(LayerHidedTrans);
                            m_FormShowed[i].Focus = false;
                            hided.Add(m_FormShowed[i]);
                        }
                    }
                    for (int i = 0; i < hided.Count; i++)
                    {
                        m_FormShowed.Remove(hided[i]);
                    }
                    m_FormHided.Push(hided);
                }

                m_FormShowed.Clear();
            }
            m_FormShowed.Add(form);
            yield return null;

        }
        public int Close(int id)
        {
            if (m_Forms.ContainsKey(id))
            {
                m_FormShowed.Remove(m_Forms[id]);

                if (m_Forms[id].Visible && m_Forms[id].IsFull && m_Forms[id].Focus)
                {
                    // 全屏而且是可见的窗体,需要显示之前被自动隐藏的界面
                    if (m_FormHided.Count > 0)
                    {
                        // 重新恢复到之前的显示
                        List<Form> showed = m_FormHided.Pop();
                        // 处理最后的界面获取焦点事件
                        if (showed.Count > 0)
                        {
                            for (int i = 0; i < showed.Count; i++)
                            {
                                Form form = showed[i];
                                form.Focus = true;
                                Transform trans = form.transform;
                                trans.SetParent(this.LayerShowedTrans[form.LayerIndex]);
                            }
                            m_FormShowed.AddRange(showed);
                        }
                    }
                }

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
        }
        protected void FormClose(object sender, int id)
        {
        }

        protected void FormLoadAssetStart(object sender, int id, string assetName)
        {
            
        }

        protected void FormLoadAssetEnd(object sender, int id, string assetName, UnityEngine.Object obj)
        {
            
        }
        protected void FormFocusChanged(object sender, int id, bool focus)
        {
        }
    }
}
