using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Ap.Base;
using Ap.Data;
using Ap.Tools;


namespace Ap.Managers
{
    // Loaded assetBundle contains the references count which can be used to unload dependent assetBundles automatically.

    // Class takes care of loading assetBundle and its dependencies automatically, loading variants automatically.
    public class AssetBundleManager : ManagerBase<AssetBundleManager>
    {

        public enum LogMode { All, JustErrors };
        public enum LogType { Info, Warning, Error };

        static LogMode m_LogMode = LogMode.All;

        protected string[] m_ActiveVariants = { };
        protected AssetBundleManifest m_AssetBundleManifest = null;

        protected Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();
        protected Dictionary<string, WWW> m_DownloadingWWWs = new Dictionary<string, WWW>();
        protected Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();
        protected List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();
        protected Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();
        //static DataTable m_AssetBundles;
        protected Dictionary<string, AssetData> m_AssetDatas = new Dictionary<string, AssetData>();
        protected Dictionary<string, AssetBundleData> m_AssetBundleDatas = new Dictionary<string, AssetBundleData>();

        public LogMode logMode
        {
            get { return m_LogMode; }
            set { m_LogMode = value; }
        }

        // The base downloading url which is used to generate the full downloading url with the assetBundle names.

        // Variants which is used to define the active variants.
        public string[] ActiveVariants
        {
            get { return m_ActiveVariants; }
            set { m_ActiveVariants = value; }
        }

        // AssetBundleManifest object which can be used to load the dependecies and check suitable assetBundle variants.
        public AssetBundleManifest AssetBundleManifestObject
        {
            set { m_AssetBundleManifest = value; }
        }

        public void Awake()
        {
            Initialize();
        }

        protected override void Init()
        {

        }

        private void Log(LogType logType, string text)
        {
            if (logType == LogType.Error)
                Debug.LogError("[AssetBundleManager] " + text);
            else if (m_LogMode == LogMode.All)
                Debug.Log("[AssetBundleManager] " + text);
        }
        // Get loaded AssetBundle, only return vaild object when all the dependencies are downloaded successfully.
        public LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
        {
            if (m_DownloadingErrors.TryGetValue(assetBundleName, out error))
                return null;

            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle == null)
                return null;

            // No dependencies are recorded, only the bundle itself is required.
            string[] dependencies = null;
            if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies))
                return bundle;

            // Make sure all dependencies are loaded
            foreach (var dependency in dependencies)
            {
                if (m_DownloadingErrors.TryGetValue(assetBundleName, out error))
                    return bundle;

                // Wait all the dependent assetBundles being loaded.
                LoadedAssetBundle dependentBundle;
                m_LoadedAssetBundles.TryGetValue(dependency, out dependentBundle);
                if (dependentBundle == null)
                    return null;
            }

            return bundle;
        }

        protected AssetBundleLoadManifestOperation Initialize()
        {
            return Initialize(Utility.GetPlatformName());
        }

        protected void InitAssetBundleDatas()
        {
            FileTable ft = new FileTable(Utility.DataPath + "/Assets.txt");
            for (int tmpi = 0; tmpi < ft.Rows.Count; tmpi++)
            {
                string[] dr = ft.Rows[tmpi];
                AssetData abd = new AssetData();
                abd.Name = dr[0].ToString();
                abd.AssetBundleName = dr[1].ToString();
                abd.Type = dr[3].ToString();
                m_AssetDatas.Add(abd.Name, abd);
            }

            ft = new FileTable(Utility.DataPath + "/AssetBundles.txt");
            for (int tmpi = 0; tmpi < ft.Rows.Count; tmpi++)
            {
                string[] dr = ft.Rows[tmpi];
                AssetBundleData abd = new AssetBundleData();
                abd.Name = dr[0].ToString();
                abd.Path = dr[1].ToString();
                abd.IsUpdate = dr[2].ToString() == "1" ? true : false;
                m_AssetBundleDatas.Add(abd.Name, abd);
            }
        }
        // Load AssetBundleManifest.
        protected AssetBundleLoadManifestOperation Initialize(string manifestAssetBundleName)
        {
            InitAssetBundleDatas();
#if UNITY_EDITOR
            // If we're in Editor simulation mode, we don't need the manifest assetBundle.

            return null;
#endif
            LoadAssetBundle(manifestAssetBundleName, true);
            var operation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
            m_InProgressOperations.Add(operation);
            return operation;
        }


        // Load AssetBundle and its dependencies.
        protected void LoadAssetBundle(string assetBundleName, bool isLoadingAssetBundleManifest = false)
        {

#if UNITY_EDITOR

            return;
#endif
            if (!isLoadingAssetBundleManifest)
            {
                if (m_AssetBundleManifest == null)
                {
                    Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                    return;
                }
            }

            // Check if the assetBundle has already been processed.
            bool isAlreadyProcessed = LoadAssetBundleInternal(assetBundleName, isLoadingAssetBundleManifest);

            // Load dependencies.
            if (!isAlreadyProcessed && !isLoadingAssetBundleManifest)
                LoadDependencies(assetBundleName);
        }

        // Remaps the asset bundle name to the best fitting asset bundle variant.
        protected string RemapVariantName(string assetBundleName)
        {
            string[] bundlesWithVariant = m_AssetBundleManifest.GetAllAssetBundlesWithVariant();

            string[] split = assetBundleName.Split('.');

            int bestFit = int.MaxValue;
            int bestFitIndex = -1;
            // Loop all the assetBundles with variant to find the best fit variant assetBundle.
            for (int i = 0; i < bundlesWithVariant.Length; i++)
            {
                string[] curSplit = bundlesWithVariant[i].Split('.');
                if (curSplit[0] != split[0])
                    continue;

                int found = System.Array.IndexOf(m_ActiveVariants, curSplit[1]);

                // If there is no active variant found. We still want to use the first 
                if (found == -1)
                    found = int.MaxValue - 1;

                if (found < bestFit)
                {
                    bestFit = found;
                    bestFitIndex = i;
                }
            }

            if (bestFit == int.MaxValue - 1)
            {
                Debug.LogWarning("Ambigious asset bundle variant chosen because there was no matching active variant: " + bundlesWithVariant[bestFitIndex]);
            }

            if (bestFitIndex != -1)
            {
                return bundlesWithVariant[bestFitIndex];
            }
            else
            {
                return assetBundleName;
            }
        }

        // Where we actuall call WWW to download the assetBundle.
        protected bool LoadAssetBundleInternal(string assetBundleName, bool isLoadingAssetBundleManifest)
        {
            // Already loaded.
            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle != null)
            {
                bundle.m_ReferencedCount++;
                return true;
            }
            if (m_DownloadingWWWs.ContainsKey(assetBundleName))
                return true;
            if (m_AssetBundleDatas.ContainsKey(assetBundleName) == false)
                return true;

            WWW download = null;
            string url = GetAssetBundlePath(assetBundleName);
            if (string.IsNullOrEmpty(url))
                return true;
           
            // For manifest assetbundle, always download it as we don't have hash for it.
            if (isLoadingAssetBundleManifest)
                download = new WWW(url);
            else
                download = WWW.LoadFromCacheOrDownload(url, m_AssetBundleManifest.GetAssetBundleHash(assetBundleName), 0);

            m_DownloadingWWWs.Add(assetBundleName, download);

            return false;
        }


        /// <summary>
        /// 获取某个assetbundle的所有依赖
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        protected string[] GetDependencies(string assetBundleName)
        {
            string[] dependencies = m_AssetBundleManifest.GetAllDependencies(assetBundleName);
            if (dependencies.Length == 0)
                return null;

            for (int i = 0; i < dependencies.Length; i++)
                dependencies[i] = RemapVariantName(dependencies[i]);

            m_Dependencies.Add(assetBundleName, dependencies);
            return dependencies;
        }
        // Where we get all the dependencies and load them all.
        protected void LoadDependencies(string assetBundleName)
        {
            if (m_AssetBundleManifest == null)
            {
                Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                return;
            }

            // Get dependecies from the AssetBundleManifest object..
            string[] dependencies = GetDependencies(assetBundleName);
            if (dependencies == null || dependencies.Length == 0)
                return;

            // Record and load all dependencies.

            for (int i = 0; i < dependencies.Length; i++)
                LoadAssetBundleInternal(dependencies[i], false);
        }


        // Unload assetbundle and its dependencies.
        public void UnloadAssetBundle(string assetBundleName)
        {
#if UNITY_EDITOR

            return;
#endif

            UnloadAssetBundleInternal(assetBundleName);
            UnloadDependencies(assetBundleName);
        }

        protected void UnloadDependencies(string assetBundleName)
        {
            string[] dependencies = null;
            if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies))
                return;

            // Loop dependencies.
            foreach (var dependency in dependencies)
            {
                UnloadAssetBundleInternal(dependency);
            }

            m_Dependencies.Remove(assetBundleName);
        }

        protected void UnloadAssetBundleInternal(string assetBundleName)
        {
            string error;
            LoadedAssetBundle bundle = GetLoadedAssetBundle(assetBundleName, out error);
            if (bundle == null)
                return;

            if (--bundle.m_ReferencedCount == 0)
            {
                bundle.m_AssetBundle.Unload(false);
                m_LoadedAssetBundles.Remove(assetBundleName);

                Log(LogType.Info, assetBundleName + " has been unloaded successfully");
            }
        }

        void Update()
        {
            // Collect all the finished WWWs.
            var keysToRemove = new List<string>();
            foreach (var keyValue in m_DownloadingWWWs)
            {
                WWW download = keyValue.Value;

                // If downloading fails.
                if (download.error != null)
                {
                    m_DownloadingErrors.Add(keyValue.Key, string.Format("Failed downloading bundle {0} from {1}: {2}", keyValue.Key, download.url, download.error));
                    keysToRemove.Add(keyValue.Key);
                    continue;
                }

                // If downloading succeeds.
                if (download.isDone)
                {
                    AssetBundle bundle = download.assetBundle;
                    if (bundle == null)
                    {
                        m_DownloadingErrors.Add(keyValue.Key, string.Format("{0} is not a valid asset bundle.", keyValue.Key));
                        keysToRemove.Add(keyValue.Key);
                        continue;
                    }

                    //Debug.Log("Downloading " + keyValue.Key + " is done at frame " + Time.frameCount);
                    m_LoadedAssetBundles.Add(keyValue.Key, new LoadedAssetBundle(download.assetBundle));
                    keysToRemove.Add(keyValue.Key);
                }
            }

            // Remove the finished WWWs.
            foreach (var key in keysToRemove)
            {
                WWW download = m_DownloadingWWWs[key];
                m_DownloadingWWWs.Remove(key);
                download.Dispose();
            }

            // Update all in progress operations
            for (int i = 0; i < m_InProgressOperations.Count; )
            {
                if (!m_InProgressOperations[i].Update())
                {
                    m_InProgressOperations.RemoveAt(i);
                }
                else
                    i++;
            }
        }


        /// <summary>
        /// 同步形式加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadAsset<T>( string assetName) where T : UnityEngine.Object
        {
            AssetData ad = m_AssetDatas[assetName];
#if UNITY_EDITOR
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(ad.AssetBundleName, assetName);
            if (assetPaths.Length == 0)
            {
                return null;
            }
            return AssetDatabase.LoadAssetAtPath<T>(assetPaths[0]);
#endif
            string assetBundleName = RemapVariantName(ad.AssetBundleName);
            string[] dependencies = GetDependencies(assetBundleName);
            if (dependencies == null || dependencies.Length == 0)
                return null;
            for (int i = 0; i < dependencies.Length; i++)
            {
                LoadAssetBundleSync(dependencies[i]);
            }
            if (m_LoadedAssetBundles.ContainsKey(assetBundleName))
            {
                LoadedAssetBundle lab = m_LoadedAssetBundles[assetBundleName];
                lab.m_ReferencedCount++;
                return lab.m_AssetBundle.LoadAsset<T>(assetName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 同步形式加载assetbundle
        /// </summary>
        /// <param name="assetBundleName"></param>
        protected void LoadAssetBundleSync(string assetBundleName)
        {
            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle != null)
            {
            }
            else
            {
                string url = GetAssetBundlePath(assetBundleName);
                if (string.IsNullOrEmpty(url))
                    return;
                if (m_DownloadingWWWs.ContainsKey(assetBundleName))
                {
                    // 处理在下载中的问题
                    // 1.先移除下载中的队列
                    // 2.移除下载错误
                    // 3.手动下载
                    m_DownloadingWWWs.Remove(assetBundleName);
                    m_DownloadingErrors.Remove(assetBundleName);
                }
                else
                {
                }
                AssetBundle ab = AssetBundle.LoadFromFile(url);
                bundle = new LoadedAssetBundle(ab);
                m_LoadedAssetBundles.Add(assetBundleName, bundle);
            }
        }

        public AssetBundleLoadAssetOperation LoadAssetAsync(string assetName, System.Type type)
        {
            if (m_AssetDatas.ContainsKey(assetName))
            {
                AssetData abd = m_AssetDatas[assetName];

                AssetBundleLoadAssetOperation operation = null;
#if UNITY_EDITOR
                string path = GetAssetBundlePath(abd.AssetBundleName);
                var res = AssetDatabase.LoadAssetAtPath(path, type);
                operation = new AssetBundleLoadAssetOperationSimulation(res);
                return operation;
#endif
                string assetBundleName = RemapVariantName(abd.AssetBundleName);
                LoadAssetBundle(assetBundleName);
                operation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);

                m_InProgressOperations.Add(operation);


                return operation;
            }
            else
            {
                Debug.LogError(string.Format("LoadAssetError: Asset {0} not found!", assetName));
                return null;
            }
        }

        // Load level from the given assetBundle.
        public AssetBundleLoadOperation LoadLevelAsync(string levelName, bool isAdditive)
        {
            AssetBundleLoadOperation operation = null;
#if UNITY_EDITOR
            throw new NoNullAllowedException();
            return null;
#endif
            if( m_AssetDatas.ContainsKey(levelName) == false )
            {
                Debug.LogError(string.Format("LoadLevelAsync: Level {0} not found!", levelName));
                return null;
            }

            AssetData ad = m_AssetDatas[levelName];
            string assetBundleName = RemapVariantName(ad.AssetBundleName);
            LoadAssetBundle(assetBundleName);
            operation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAdditive);

            m_InProgressOperations.Add(operation);
            
            return operation;
        }
        /// <summary>
        /// 获取assetbundle的路径
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        private string GetAssetBundlePath(string assetBundleName )
        {
            if (m_AssetBundleDatas.ContainsKey(assetBundleName) == false)
                return "";
            else
            {
                AssetBundleData abd = m_AssetBundleDatas[assetBundleName];
                if (abd.IsUpdate)
                    return Utility.AssetBundleUpdatePath + assetBundleName;
                else
                    return Utility.AssetBundlePath + assetBundleName;
            }
        }
    }

}
