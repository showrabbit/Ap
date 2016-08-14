using UnityEngine;
using System.Collections;


namespace Ap.Base
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBase<T> where T : new()
    {
        private static T mT;

        public static T Instance
        {
            get
            {
                if (mT == null)
                {
                    mT = new T();
                }
                return mT;
            }
        }

        protected SingletonBase()
        {
            Init();
        }

        protected abstract void Init();


    }

}
