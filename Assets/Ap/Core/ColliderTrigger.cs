using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Ap.Core
{
    public class ColliderTrigger : MonoBehaviourEx
    {

        public delegate void OnEnterHandle(Collider2D collision);
        public OnEnterHandle OnEnter;
        public delegate void OnExitHandle(Collider2D collision);
        public OnExitHandle OnExit;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (OnEnter != null)
                OnEnter(collision);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (OnExit != null)
            {
                OnExit(collision);
            }
        }

    }


}
