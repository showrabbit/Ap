using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Base
{

    public class EventData
    {
        public object[] Values;
        public bool IsHandled = false;
        public EventData()
        {

        }
        public EventData(params object[] value)
        {
            this.Values = value;
        }

    }
}


