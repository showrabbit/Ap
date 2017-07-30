using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using UnityEngine;

namespace Ap.Net
{
    /// <summary>
    /// web 客户端
    /// </summary>
    public class HttpClient : NetClient
    {


        public HttpClient()
        {
        }

        /// <summary>
        /// 发送
        /// </summary>
        public WWW PostWithWWW(string url, Dictionary<string, string> values)
        {
            WWWForm form = new WWWForm();
            foreach (var v in values)
            {
                form.AddField(v.Key, v.Value);
            }
            WWW www = new WWW(url, form);
            return www;
        }

        public byte[] Post(string url, Dictionary<string, string> values)
        {
            WWWForm form = new WWWForm();
            foreach (var v in values)
            {
                form.AddField(v.Key, v.Value);
            }
            WWW www = new WWW(url, form);
            while (www.isDone)
            {
            }
            if (string.IsNullOrEmpty(www.error))
            {
                return www.bytes;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取
        /// </summary>
        public WWW GetWithWWW(string url)
        {
            WWW www = new WWW(url);
            return www;
        }

        public byte[] Get(string url)
        {
            WWW www = new WWW(url);
            while (www.isDone)
            {
            }
            if (string.IsNullOrEmpty(www.error))
            {
                return www.bytes;
            }
            else
                return null;
        }

        protected IEnumerator Quary(WWW www)
        {
            if (QuaryStart != null)
            {
                QuaryStart(this, new NetQuaryEvent());
            }

            yield return www;

            NetQuaryEvent e = new NetQuaryEvent();
            if (www.error != null)
            {
                e.Error = www.error;
            }
            else
            {
                e.Data = www.bytes;
            }
            if (QuaryEnd != null)
            {
                QuaryEnd(this, e);
            }
            yield return null;
        }
    }
}
