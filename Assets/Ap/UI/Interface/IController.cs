using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Core
{
    public interface IController
    {
        void BindView(IView view);
    }
}
