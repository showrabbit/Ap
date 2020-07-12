using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

namespace Ap.UI.Depth
{
    /// <summary>
    /// 绘制到纹理对象
    /// </summary>
    public class UIIDepthRttTarget : MonoBehaviour
    {
        /// <summary>
        /// 最终显示的图片
        /// </summary>
        public UnityEngine.UI.RawImage TargetImg;

        /// <summary>
        /// 背景图片
        /// </summary>
        public Image BackImg;

        /// <summary>
        /// 模型对象
        /// </summary>
        public GameObject ModelObj;


        protected void Awake()
        {

        }


        protected void OnDestroy()
        {

        }

    }
}
