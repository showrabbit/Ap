using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Managers;
using System.Collections;
using Ap.Base;

namespace Ap.UI
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
