﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Base
{
    public interface IView
    {
        void Changed(string type,params object[] values);
    }
}
