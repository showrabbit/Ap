using Ap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Res
{
    public class CompressBase : MonoBehaviourEx
    {
        public delegate void CompressHandle(object sender, int progress, string error);

        public delegate void DecompressHandle(object sender, int progress, string error);



        public CompressHandle OnCompress;

        public DecompressHandle OnDecompress;




    }
}
