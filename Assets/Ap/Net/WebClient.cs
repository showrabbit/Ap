using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Ap.Net
{
    /// <summary>
    /// web 客户端
    /// </summary>
    public class WebClient
    {
        /// <summary>
        /// 连接串
        /// </summary>
        public string Url
        {
            get
            {
                return m_Url;
            }
            set
            {
                m_Url = value;
            }

        }
        private string m_Url = "";

        /// <summary>
        /// 超时
        /// </summary>
        public int TimeOut
        {
            set;
            get;
        }

        

        public WebClient(string url)
        {
            m_Url = url;
        }

        /// <summary>
        /// 发送
        /// </summary>
        public void Post()
        {
            
        }

        /// <summary>
        /// 获取
        /// </summary>
        public void Get()
        {

        }
    }
}
