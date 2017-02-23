using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace Ap.Tools
{
    public static class UIHelper
    {

        /// <summary>
        /// 设置RectTransform设置为占满
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public static void RectTransformSetFull(RectTransform rect)
        {
            rect.localScale = Vector3.one;
            rect.localPosition = Vector3.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }
}
