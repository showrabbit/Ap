using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ap.UI.Core
{
    public class Joystick : ScrollRectEx
    {
        public delegate void OnDragHandle(object sender, Vector3 dir);

        public OnDragHandle OnDragEvent;

        protected RectTransform mChild;

        protected float mRadius;
        protected bool mIsDraged = false;
        protected override void Start()
        {
            //计算摇杆块的半径
            mChild = transform.GetChild(0) as RectTransform;
            mRadius = (transform as RectTransform).sizeDelta.x * 0.5f - mChild.sizeDelta.x * 0.5f;
        }

        public void Update()
        {
            if (mIsDraged)
            {
                if (OnDragEvent != null)
                {
                    OnDragEvent(this, mChild.transform.localPosition);
                }
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            mIsDraged = true;
            base.OnBeginDrag(eventData);
        }
        public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnDrag(eventData);
            var contentPostion = this.content.anchoredPosition;
            if (contentPostion.magnitude > mRadius)
            {
                contentPostion = contentPostion.normalized * mRadius;
                SetContentAnchoredPosition(contentPostion);
            }

        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            mIsDraged = false;
            base.OnEndDrag(eventData);
        }

        /// <summary>
        /// 获取方向
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDirection()
        {
            return mChild.localPosition;
        }

    }

}
