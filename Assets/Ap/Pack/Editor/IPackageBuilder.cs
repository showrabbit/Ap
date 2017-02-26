using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ap.Pack
{
    interface IPackageBuilder
    {
        void BuildLua(string path);
        //void BuildData(string path);

        void BuildAssetBundle(string path);
        void BuildPlayer(string path);
        void UpLoad(string url);
        void Clear();
    }
}
