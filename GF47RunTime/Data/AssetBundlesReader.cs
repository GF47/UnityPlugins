/* ****************************************************************
 * @File Name   :   AssetBundlesReader
 * @Author      :   GF47
 * @Date        :   2015/4/22 10:49:20
 * @Description :   AssetBundle的读取类
 * @Edit        :   2015/4/22 10:49:20
 * ***************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GF47RunTime.Data
{
    /// <summary>
    /// AssetBundle的读取类
    /// </summary>
    public class AssetBundlesReader : MonoBehaviour, IDisposable
    {

        public static readonly string PathURL = 
#if UNITY_ANDROID
            string.Format("jar:file://{0}!/assets/", Application.dataPath);
#elif UNITY_IPHONE
            string.Format("{0}/Raw/", Application.dataPath);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
            string.Format("file://{0}/", Application.streamingAssetsPath);
#endif

        public static readonly string UncompressedPath = string.Format("{0}/", Application.streamingAssetsPath);

        public static AssetBundlesReader Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject instance = new GameObject("__AssetBundleManager");
                    DontDestroyOnLoad(instance);
                    _instance = instance.AddComponent<AssetBundlesReader>();
                }
                return _instance;
            }
        }
        private static AssetBundlesReader _instance;

        /// <summary>
        /// 包里的主资源的实例
        /// </summary>
        public GameObject mainAsset;
        /// <summary>
        /// 包里的资源实例列表
        /// </summary>
        public List<GameObject> assets;

        /// <summary>
        /// 加载的图片
        /// </summary>
        public Texture texture;

        public ScriptableObject scriptableObject;

        /// <summary>
        /// 异步加载一个包的主资源
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="version">包版本</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        public void LoadMainAssetAsync(string path, int version, Action<AssetBundle, EventArgs> callBack)
        {
            StartCoroutine(_loadMainAssetAsync(path, version, callBack));
        }
        private IEnumerator _loadMainAssetAsync(string path, int version, Action<AssetBundle, EventArgs> callBack)
        {
            WWW bundle = WWW.LoadFromCacheOrDownload(path, version);
            yield return bundle;
            if (bundle == null)
            {
                mainAsset = null;
                yield break;
            }
            mainAsset = Instantiate(bundle.assetBundle.mainAsset) as GameObject;
            yield return mainAsset;
            if (callBack != null) callBack(bundle.assetBundle, null);
            bundle.assetBundle.Unload(false);
            bundle.Dispose();
        }
        /// <summary>
        /// 同步加载一个包的主资源，包必须是未压缩过的
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        /// <returns>主资源的实例</returns>
        public GameObject LoadMainAssetUncompressed(string path, Action<AssetBundle, EventArgs> callBack)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                mainAsset = null;
                return null;
            }
            mainAsset = Instantiate(bundle.mainAsset) as GameObject;
            if (callBack != null) callBack(bundle, null);
            bundle.Unload(false);
            return mainAsset;
        }

        public void LoadMainAssetAsyncAs<T>(string path, int version, Action<AssetBundle, EventArgs> callback) where T : ScriptableObject
        {
            StartCoroutine(_loadMainAssetAsyncAs<T>(path, version, callback));
        }
        private IEnumerator _loadMainAssetAsyncAs<T>(string path, int version, Action<AssetBundle, EventArgs> callback) where T : ScriptableObject
        {
            WWW bundle = WWW.LoadFromCacheOrDownload(path, version);
            yield return bundle;
            if (bundle == null)
            {
                yield break;
            }
            scriptableObject = bundle.assetBundle.mainAsset as T;
            yield return scriptableObject;
            if (callback != null)
            {
                callback(bundle.assetBundle, null);
            }
            bundle.assetBundle.Unload(false);
            bundle.Dispose();
        }

        public T LoadMainAssetUncompressedAs<T>(string path, Action<AssetBundle, EventArgs> callback) where T : ScriptableObject
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                scriptableObject = null;
                return null;
            }
            scriptableObject = bundle.mainAsset as T;
            if (callback != null)
            {
                callback(bundle, null);
            }
            bundle.Unload(false);
            return scriptableObject as T;
        }

        /// <summary>
        /// 根据名称列表异步加载一个包的资源
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="version">版本</param>
        /// <param name="assetNames">名称列表</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        /// <returns></returns>
        public void LoadAssetsAsync(string path, int version, IList<string> assetNames, Action<AssetBundle, EventArgs> callBack)
        {
            StartCoroutine(_loadAssetsAsync(path, version, assetNames, callBack));
        }
        private IEnumerator _loadAssetsAsync(string path, int version, IList<string> assetNames, Action<AssetBundle, EventArgs> callBack)
        {
            WWW bundle = WWW.LoadFromCacheOrDownload(path, version);
            yield return bundle;
            if (bundle == null)
            {
                assets = null;
                yield break;
            }
            if (assets == null) assets = new List<GameObject>();
            assets.Clear();
            for (int i = 0,iMax = assetNames.Count; i < iMax; i++)
            {
                GameObject temp = Instantiate(bundle.assetBundle.LoadAsset<GameObject>(assetNames[i]));
                if (temp != null) assets.Add(temp);
            }
            yield return assets;
            if (callBack != null) callBack(bundle.assetBundle, null);
            bundle.assetBundle.Unload(false);
            bundle.Dispose();
        }

        /// <summary>
        /// 根据名称列表同步加载一个包的资源，包必须是未压缩的
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="assetNames">名称列表</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        /// <returns>资源实例的列表</returns>
        public List<GameObject> LoadAssetsUncompressed(string path, IList<string> assetNames, Action<AssetBundle, EventArgs> callBack)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                assets = null;
                return null;
            }
            if (assets == null) assets = new List<GameObject>();
            assets.Clear();
            for (int i = 0, iMax = assetNames.Count; i < iMax; i++)
            {
                GameObject temp = Instantiate(bundle.LoadAsset<GameObject>(assetNames[i]));
                if (temp != null) assets.Add(temp);
            }
            if (callBack != null) callBack(bundle, null);
            bundle.Unload(false);
            return assets;
        }

        /// <summary>
        /// 异步加载一个包的所有[GameObject]类型的资源
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="version">版本</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        /// <returns></returns>
        public void LoadAllGameObjectAssetsAsync(string path, int version, Action<AssetBundle, EventArgs> callBack)
        {
            StartCoroutine(_loadAllGameObjectAssetsAsync(path, version, callBack));
        }
        private IEnumerator _loadAllGameObjectAssetsAsync(string path, int version, Action<AssetBundle, EventArgs> callBack)
        {
            WWW bundle = WWW.LoadFromCacheOrDownload(path, version);
            yield return bundle;
            if (bundle == null)
            {
                assets = null;
                yield break;
            }
            if (assets == null) assets = new List<GameObject>();
            else assets.Clear();
            GameObject[] objs = bundle.assetBundle.LoadAllAssets<GameObject>();
            for (int i = 0, iMax = objs.Length; i < iMax; i++)
            {
                GameObject temp = Instantiate(objs[i]);
                if (temp != null) assets.Add(temp);
            }
            yield return assets;
            if (callBack != null) callBack(bundle.assetBundle, null);
            bundle.assetBundle.Unload(false);
            bundle.Dispose();
        }

        /// <summary>
        /// 同步加载一个包的所有[GameObject]类型的资源
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="callBack">加载完毕后执行的事件</param>
        /// <returns>资源实例的列表</returns>
        public List<GameObject> LoadAllGameObjectAssetsUncompressed(string path, Action<AssetBundle, EventArgs> callBack)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                assets = null;
                return null;
            }
            if (assets == null) assets = new List<GameObject>();
            assets.Clear();
            GameObject[] objs = bundle.LoadAllAssets<GameObject>();
            for (int i = 0, iMax = objs.Length; i < iMax; i++)
            {
                GameObject temp = Instantiate(objs[i]);
                if (temp != null) assets.Add(temp);
            }
            if (callBack != null) callBack(bundle, null);
            bundle.Unload(false);
            return assets;
        }

        public void LoadTextureAsync(string path, Action<AssetBundle, EventArgs> callBack)
        {
            StartCoroutine(_loadTextureAsync(path, callBack));
        }
        private IEnumerator _loadTextureAsync(string path, Action<AssetBundle, EventArgs> callBack)
        {
            WWW www = new WWW(path);
            yield return www;

            texture = www.texture;
            if (texture != null && callBack != null) callBack(null, new UnityEventArgs<Texture>(texture));

            www.Dispose();
        }

        public void LoadTexture(string path, Action<AssetBundle, EventArgs> callBack)
        {
            byte[] bt = File.ReadAllBytes(path);
            Texture2D t = new Texture2D(1,1);
            t.LoadImage(bt);
            t.EncodeToPNG();
            texture = t;
            if (texture != null && callBack != null) callBack(null, new UnityEventArgs<Texture>(texture));
        }

        // TODO 加载其它类型的资源

        #region Dispose
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 托管
                    mainAsset = null;
                    assets = null;
                    texture = null;
                }
                // 非托管
                _disposed = true;
            }
        }
        void OnDestroy() 
        {
            Dispose(false);
        }
        #endregion
    }
}
