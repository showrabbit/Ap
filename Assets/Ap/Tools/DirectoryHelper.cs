using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ap.Tools
{
    /// <summary>
    /// 目录操作帮助
    /// </summary>
    public class DirectoryHelper
    {
        /// <summary>
        /// 创建一个目录
        /// 如果已经存在就不在创建
        /// </summary>
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }
        
    }
}
