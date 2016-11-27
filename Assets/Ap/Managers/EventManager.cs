using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.Base;

namespace Ap.Managers
{
    /// <summary>
    /// 事件管理 
    /// 通过这个类进行事件的转发
    /// </summary>
    public class EventManager : ManagerBase<EventManager>, IEventTrigge
    {

        public delegate void OnEventHandle(object sender, EventData data);

        private Dictionary<int, List<OnEventHandle>> mHandles = new Dictionary<int, List<OnEventHandle>>();

        protected override void Init()
        {

        }

        public void AddHandle(int type, OnEventHandle handle)
        {
            if (!mHandles.ContainsKey(type))
            {
                mHandles.Add(type, new List<OnEventHandle>());
            }
            mHandles[type].Add(handle);
        }
        public void RemoveHandle(int type, OnEventHandle handle)
        {
            mHandles[type].Remove(handle);
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void Trigge(int type, object sender, EventData data)
        {
            if (mHandles.ContainsKey(type))
            {
                for (int i = 0; i < mHandles[type].Count; i++)
                {
                    mHandles[type][i].Invoke(sender, data);
                    bool isHandled = data.IsHandled;
                    if (isHandled)
                    {
                        break;
                    }
                }
            }
        }

        
    }

}
