using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Ap.Core;

namespace Ap.Net
{
    public class NetQuaryEvent
    {
        public string Error;
        public byte[] Data;

        public NetQuaryEvent()
        {

        }
        public NetQuaryEvent(string error,byte[] data)
        {
            this.Error = error;
            this.Data = data;
        }
    }

    public class NetClient : MonoBehaviourEx
    {
        public delegate void OnQuaryStartHandle(object sender, NetQuaryEvent e);
        public delegate void OnQuaryEndHandle(object sender, NetQuaryEvent e);

        public OnQuaryStartHandle QuaryStart;
        public OnQuaryEndHandle QuaryEnd;
    }
}
