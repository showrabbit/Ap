using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Base;
namespace Ap.Managers
{
    public class ManagerManagers : SingletonBase<ManagerManagers>
    {
        public AssetBundleManager A
        {
            get
            {
                return AssetBundleManager.Instance;
            }
        }
        
        public EventManager E
        {
            get
            {
                return EventManager.Instance;
            }
        }

        public FormManager F
        {
            get
            {
                return FormManager.Instance;
            }
        }

        public GameManager G
        {
            get
            {
                return GameManager.Instance;
            }
        }

        public LuaManager L
        {
            get
            {
                return LuaManager.Instance;
            }
        }

        public NetworkManager N
        {
            get
            {
                return NetworkManager.Instance;
            }
        }

        public ObjectPoolManager O
        {
            get
            {
                return ObjectPoolManager.Instance;
            }
        }


        protected override void Init()
        {
            var e = EventManager.Instance;
            var a = AssetBundleManager.Instance;
            var f = FormManager.Instance;
            var g = GameManager.Instance;
            var l = LuaManager.Instance;
            var n = NetworkManager.Instance;
            var o = ObjectPoolManager.Instance;
        }

        public override void Clear()
        {
            
        }
    }
}
