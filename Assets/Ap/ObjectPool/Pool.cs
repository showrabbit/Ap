using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ap.ObjectPool
{
    public class Pool
    {

        public GameObject TemplateObject;
        public int MaxCount;
        public List<Cache> CacheObjects;
        public GameObject PoolRoot;
    }
}
