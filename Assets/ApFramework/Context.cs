﻿using UnityEngine;
using System.Collections;
using Ap.Base;
using Ap.Managers;

namespace Ap
{
    public class Context : SingletonBase<Context>
    {

        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ServerIp
        {
            get;
            set;

        }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort
        {
            get;
            set;
        }

        

        protected override void Init()
        {
#if UNITY_EDITOR
            //AssetBundleManager.Initialize();
#else
        StartCoroutine(AssetBundleManager.Initialize());
#endif
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        
    }

}
