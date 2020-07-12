using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Event
{
    public interface IEventTrigge
    {
        void Trigge(int type, object sender, EventData data);
    }

}

