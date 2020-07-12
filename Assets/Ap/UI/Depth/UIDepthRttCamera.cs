using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Ap.UI.Depth
{
    /// <summary>
    /// 绘制到纹理
    /// </summary>
    public class UIDepthRttCamera : MonoBehaviour
    {
        /// <summary>
        /// 摄像机
        /// </summary>
        public Camera Camera;
        

        public GameObject[] Targets;


        public static UIDepthRttCamera Instanst
        {
            get;
            set;
        }


        public void OnPostRender()
        {
            if( Targets != null )
            {
                for (int i = 0; i < Targets.Length; i++)
                {
                    var t = Targets[i];
                    t.SetActive(true);

                    var tt = Camera.targetTexture;
                    Camera.Render();
                    RenderTexture.active = tt;

                    Texture2D tex = new Texture2D(tt.width, tt.height, TextureFormat.ARGB32, false);

                    tex.ReadPixels(new Rect(0, 0, tt.width, tt.height), 0, 0);
                    tex.Apply();

                }

                
            }
            
        }



    }
}
