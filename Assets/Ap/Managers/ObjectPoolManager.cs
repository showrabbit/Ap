using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ap.ObjectPool;
using Ap.Base;

namespace Ap.Managers
{
    /// <summary>
    /// 对象池管理
    /// 管理对象缓存
    /// </summary>
    public class ObjectPoolManager : ManagerBase<ObjectPoolManager>
    {
        protected Dictionary<string, Pool> mPools = new Dictionary<string, Pool>();

        protected GameObject mPoolManagerRoot = null;

        public void Awake()
        {
            mPoolManagerRoot = new GameObject("PoolManager");
            Object.DontDestroyOnLoad(mPoolManagerRoot);
        }

        protected override void Init()
        {
            
        }

        public void AddPool(string poolName, GameObject templateObj, int maxCount)
        {
            Pool pool = new Pool();
            pool.TemplateObject = templateObj;
            pool.MaxCount = maxCount;
            pool.CacheObjects = new List<Cache>();
            pool.PoolRoot = new GameObject(poolName);
            pool.PoolRoot.transform.SetParent(mPoolManagerRoot.transform);
            templateObj.transform.SetParent(pool.PoolRoot.transform);
            Object.DontDestroyOnLoad(pool.PoolRoot);

            mPools.Add(poolName, pool);
        }
        public void RemovePool(string poolName)
        {
            if (mPools.ContainsKey(poolName))
            {
                Pool pool = mPools[poolName];
                // 确保现在正在使用的对象
                for (int tmpi = 0; tmpi < pool.CacheObjects.Count; tmpi++)
                {
                    Cache cache = pool.CacheObjects[tmpi];
                    if (cache.IsUsed)
                    {
                        GameObject.Destroy(cache.Object);
                    }
                }

                GameObject.Destroy(pool.PoolRoot);
                mPools.Remove(poolName);
            }
        }

        public GameObject AddCache(string poolName)
        {
            Pool pool = mPools[poolName];

            for (int tmpi = 0; tmpi < pool.CacheObjects.Count; tmpi++)
            {
                if (pool.CacheObjects[tmpi].IsUsed == false)
                {
                    pool.CacheObjects[tmpi].IsUsed = true;
                    return pool.CacheObjects[tmpi].Object;
                }
            }
            Cache cache = new Cache();
            cache.Object = GameObject.Instantiate(pool.TemplateObject);
            cache.IsUsed = true;
            pool.CacheObjects.Add(cache);
            return cache.Object;
        }

        public void RemoveCache(string poolName, GameObject cache)
        {
            Pool pool = mPools[poolName];
            for (int tmpi = 0; tmpi < pool.CacheObjects.Count; tmpi++)
            {
                if (pool.CacheObjects[tmpi].Object == cache)
                {
                    pool.CacheObjects[tmpi].IsUsed = false;
                    pool.CacheObjects[tmpi].Object.transform.SetParent(pool.PoolRoot.transform);
                }
            }
        }
    }
}



