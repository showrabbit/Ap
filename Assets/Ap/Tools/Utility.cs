using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ap.Tools
{
    public class Utility
    {
        /// <summary>
        /// ���������Ӷ���
        /// </summary>
        public static void GameObjectDestroyChild(GameObject obj, int startIndex = 0)
        {
            for (int tmpi = startIndex; tmpi < obj.transform.childCount - 1; tmpi++)
            {
                GameObject.Destroy(obj.transform.GetChild(tmpi).gameObject);
            }
        }
    }

}
