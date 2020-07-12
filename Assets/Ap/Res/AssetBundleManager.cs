using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using Ap.Core;
using Ap.Data;
using Ap.Tools;


namespace Ap.Res
{

    // Class takes care of loading assetBundle and its dependencies automatically, loading variants automatically.
    public class AssetBundleManager : ManagerBase<AssetBundleManager>
    {

        public enum LogMode { All, JustErrors };
        public enum LogType { Info, Warning, Error };

        static LogMode m_LogMode = LogMode.All;

        protected string[] m_ActiveVariants = { };
        protected AssetBundleManifest m_AssetBundleManifest = null;

        protected Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();
        protected Dictionary<string,string> m_DownloadingBundles = new Dictionary<string,string>();
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

        private AssetBundleLoadManifestOperation m_AssetBundleManifestOpt;
        /// <summary>
        /// manifest 操作回调 
        /// 确保加载manifest之后才能正式开始进行游戏
        /// </summary>
        public AssetBundleLoadManifestOperation AssetBundleManifestOpt
        {
            get
            {
                return m_AssetBundleManifestOpt;
            }
        }


        public void Awake()
        {
            m_AssetBundleManifestOpt = Initialize();
        }
        protected AssetBundleLoadManifestOperation Initialize()
        {
            return Initialize(Environment.GetPlatformName());
        }

        protected void InitAssetBundleDatas()
        {
            FileTable ft = new FileTable(Environment.DataPath + "/Assets.txt");
            for (int tmpi = 0; tmpi < ft.Rows.Count; tmpi++)
            {
                string[] dr = ft.Rows[tmpi];
                AssetData abd = new AssetData();
                abd.Name = dr[0].ToString();
                abd.AssetBundleName = dr[1].ToString();
                abd.Type = dr[2].ToString();
                m_AssetDatas.Add(abd.Name, abd);
            }

            ft = new FileTable(Environment.DataPath + "/AssetBundles.txt");
            for (int tmpi = 0; tmpi < ft.Rows.Count; tmpi++)
            {
                string[] dr = ft.Rows[tmpi];
                AssetBundleData abd = new AssetBundleData();
                abd.Name = dr[0].ToString();
                abd.IsUpdate = dr[1].ToString() == "1" ? true : false;
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
            //LoadAssetBundle(manifestAssetBundleName, true);
            string path = Ap.Core.Environment.AssetBundleUpdatePath + "/" + manifestAssetBundleName;
            m_InProgressOperations.Add(new AssetBundleDownloadFromFileOperation(manifestAssetBundleName, path));
            m_DownloadingBundles.Add(manifestAssetBundleName);

            var operation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
            m_InProgressOperations.Add(operation);
            return operation;
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
            bool isAlreadyProcessed = LoadAssetBundleInternal(assetBundleName);

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
        protected bool LoadAssetBundleInternal(string assetBundleName)
        {
            // Already loaded.
            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle != null)
            {
                bundle.m_ReferencedCount++;
                return true;
            }


            if (this.m_DownloadingBundles.Contains(assetBundleName))
                return true;
            if (m_AssetBundleDatas.ContainsKey(assetBundleName) == false)
                return true;


            string path = GetAssetBundlePath(assetBundleName);
            if (string.IsNullOrEmpty(path))
                return true;

            m_InProgressOperations.Add(new AssetBundleDownloadFromFileOperation(assetBundleName, path));

            m_DownloadingBundles.Add(assetBundleName);

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
                LoadAssetBundleInternal(dependencies[i]);
        }

        /// <summary>
        /// 根据asset名称卸载
        /// </summary>
        /// <param name="assetName"></param>
        public void UnloadAssetBundleByAssetName(string assetName)
        {
#if UNITY_EDITOR

            return;
#endif
            if (m_AssetDatas.ContainsKey(assetName))
            {
                AssetData abd = m_AssetDatas[assetName];

                string assetBundleName = RemapVariantName(abd.AssetBundleName);
                UnloadAssetBundle(assetBundleName);
            }
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
            for (int i = 0; i < m_InProgressOperations.Count;)
            {
                var operation = m_InProgressOperations[i];
                if (operation.Update())
                {
                    i++;
                }
                else
                {
                    m_InProgressOperations.RemoveAt(i);
                    ProcessFinishedOperation(operation);
                }
            }
        }
        void ProcessFinishedOperation(AssetBundleLoadOperation operation)
        {
            AssetBundleDownloadOperation download = operation as AssetBundleDownloadOperation;
            if (download == null)
                return;

            if (download.error == null)
                m_LoadedAssetBundles.Add(download.assetBundleName, download.assetBundle);
            else
            {
                string msg = string.Format("Failed downloading bundle {0} from {1}: {2}",
                        download.assetBundleName, download.assetBundleName, download.error);
                m_DownloadingErrors.Add(download.assetBundleName, msg);
            }

            m_DownloadingBundles.Remove(download.assetBundleName);
        }

        /// <summary>
        /// 同步形式加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
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
                if (m_DownloadingBundles.ContainsKey(assetBundleName))
                {
                    // 处理在下载中的问题
                    // 1.先移除下载中的队列
                    // 2.移除下载错误
                    // 3.手动下载
                    m_DownloadingBundles.Remove(assetBundleName);
                    m_DownloadingErrors.Remove(assetBundleName);
                    for (int i = 0; i < m_InProgressOperations.Count; i++)
                    {
                        if (m_InProgressOperations[i] is AssetBundleDownloadFromFileOperation)
                        {
                            AssetBundleDownloadFromFileOperation op = m_InProgressOperations[i] as AssetBundleDownloadFromFileOperation;
                            if (op.assetBundleName == assetBundleName)
                            {
                                m_InProgressOperations.RemoveAt(i);
                                break;
                            }
                        }
                    }
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
                string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(abd.AssetBundleName, assetName);
                if (assetPaths.Length == 0)
                {
                    return null;
                }
                var res = AssetDatabase.LoadAssetAtPath(assetPaths[0], type);
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
            return null;
#endif
            if (m_AssetDatas.ContainsKey(levelName) == false)
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
        private string GetAssetBundlePath(string assetBundleName)
        {
            if (m_AssetBundleDatas.ContainsKey(assetBundleName) == false)
                return "";
            else
            {
                AssetBundleData abd = m_AssetBundleDatas[assetBundleName];
                if (abd.IsUpdate)
                    return Environment.AssetBundleUpdatePath + assetBundleName;
                else
                    return Environment.AssetBundlePath + assetBundleName;
            }
        }
    }

}
