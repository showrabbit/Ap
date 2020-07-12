using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Ap.Core;

namespace Ap.UI.Core
{
    public class FormCtr : MonoBehaviourEx, IController
    {
        IView m_View = null;

        public void BindView(IView view)
        {
            m_View = view;
        }

    }
}
