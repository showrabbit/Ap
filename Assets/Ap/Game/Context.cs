using UnityEngine;
using System.Collections;
using Ap.Core;

namespace Ap.Game
{
    /// <summary>
    /// 全局上下文
    /// 
    /// </summary>
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

        /// <summary>
        /// 是否是小端序的数据
        /// </summary>
        public bool IsLittleEndian = true;



        protected override void Init()
        {

        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Clear()
        {
            
        }
    }

}
