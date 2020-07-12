using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Ap.UI.Depth
{

    /// <summary>
    /// UI 深度
    /// </summary>
    interface IUIDepth
    {
        /// <summary>
        /// 设置深度
        /// </summary>
        /// <param name="depth"></param>
        void SetDepth(int depth);
        /// <summary>
        /// 获取深度
        /// </summary>
        /// <returns></returns>
        int GetDepth();

    }
}



