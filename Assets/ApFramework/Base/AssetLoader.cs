using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ap.Managers;
using UnityEngine;

namespace Ap.Base
{
    public class AssetLoader : MonoBehaviourEx
    {
        public void LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            StartCoroutine(LoadAssetAsync<T>(assetName));
        }

        public IEnumerator LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
            AssetBundleLoadAssetOperation abl = AssetBundleManager.Instance.LoadAssetAsync(assetName, typeof(T));
            OnLoadAssetStart(assetName);
            yield return abl;
            OnLoadAssetEnd(assetName, abl.GetAsset<T>());
        }

        protected virtual void OnLoadAssetStart(string assetName)
        {

        }

        protected virtual void OnLoadAssetEnd(string assetName, UnityEngine.Object obj)
        {

        }
    }
}
