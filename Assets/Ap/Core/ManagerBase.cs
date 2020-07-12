using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Core;
using UnityEngine;
namespace Ap.Core
{
    public abstract class ManagerBase<T> : MonoBehaviourEx where T : MonoBehaviourEx, new()
    {
        private static T m_T;

        public static T Instance
        {
            get
            {
                if (m_T == null)
                {
                    m_T = CreateManager();
                }
                return m_T;
            }
        }
        public ManagerBase()
        {
            Init();
        }
        /// <summary>
        /// 创建一个管理
        /// </summary>
        /// <returns></returns>
        private static T CreateManager()
        {
            GameObject goManager = GameObject.Find("Managers");
            if (goManager == null)
            {
                goManager = new GameObject("Managers");
                DontDestroyOnLoad(goManager);
            }
            return goManager.AddComponent<T>();
        }

        protected abstract void Init();


    }

}

