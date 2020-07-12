using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Core;
using UnityEngine;
namespace Ap.Tools
{

    public enum LogLevel
    {
        Normal,
        Warn,
        Error
    }

    /// <summary>
    /// 日志类
    /// </summary>
    public class Logger : SingletonBase<Logger>
    {

        protected override void Init()
        {

        }

        public override void Clear()
        {

        }

        public void Write(string msg)
        {
            Write(LogLevel.Normal, msg);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="msg">消息</param>
        public void Write(LogLevel level, string msg)
        {
            //
            //todo 后期完善记录文本,上传服务器等功能
            if (String.IsNullOrEmpty(msg))
                msg = "null";
            switch(level)
            {
                case LogLevel.Normal:
                    {
                        Debug.LogFormat(msg);
                    }break;
                case  LogLevel.Warn:
                    {
                        Debug.LogFormat(msg);
                    }break;
                case LogLevel.Error:
                    {
                        Debug.LogFormat(msg);
                    }break;
            }
        }

        public void Write(int level,string msg)
        {
            Write((LogLevel)level, msg);
        }
    }
}
