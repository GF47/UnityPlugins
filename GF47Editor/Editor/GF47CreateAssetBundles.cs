/* ****************************************************************
 * @File Name   :   GF47CreateAssetBundles
 * @Author      :   GF47
 * @Date        :   2015/4/21 17:35:30
 * @Description :   to do
 * @Edit        :   2015/4/21 17:35:30
 * ***************************************************************/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace GF47Editor.Editor
{
    /// <summary>
    /// A example class in Unity
    /// </summary>
    public class GF47CreateAssetBundles : ScriptableObject
    {
        [MenuItem("Assets/GF47 Editor/AssetBundles/Create Single Uncompressed")]
        private static void CreateSingleUncompressedAssetBundles()
        {
            Caching.CleanCache();

            if (!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);

            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            for (int i = 0, iMax = selectedAssets.Length; i < iMax; i++)
            {
                string targetPath = string.Format("{0}/{1}.assetbundle", Application.streamingAssetsPath, selectedAssets[i].name);
#if     UNITY_ANDROID
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android))
#elif   UNITY_IPHONE
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iPhone))
#elif   UNITY_STANDALONE_WIN || UNITY_EDITOR
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle,BuildTarget.StandaloneWindows))
#else
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle))
#endif
                    Debug.Log(string.Format("{0}资源打包成功", selectedAssets[i].name));
                else
                    Debug.LogWarning(string.Format("{0}资源打包失败", selectedAssets[i].name));
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Assets/GF47 Editor/AssetBundles/Create Single Compressed")]
        private static void CreateSingleCompressedAssetBundles()
        {
            Caching.CleanCache();

            if (!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);

            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

            for (int i = 0, iMax = selectedAssets.Length; i < iMax; i++)
            {
                string targetPath = string.Format("{0}/{1}.assetbundle", Application.streamingAssetsPath, selectedAssets[i].name);
#if     UNITY_ANDROID
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android))
#elif   UNITY_IPHONE
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone))
#elif   UNITY_STANDALONE_WIN || UNITY_EDITOR
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows))
#else
                if (BuildPipeline.BuildAssetBundle(selectedAssets[i], null, targetPath, BuildAssetBundleOptions.CollectDependencies))
#endif
                    Debug.Log(string.Format("{0}资源打包成功", selectedAssets[i].name));
                else
                    Debug.LogWarning(string.Format("{0}资源打包失败", selectedAssets[i].name));
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Assets/GF47 Editor/AssetBundles/Create Combined Uncompressed")]
        private static void CreateCombinedUncompressedAssetBundles()
        {
            Caching.CleanCache();

            if (!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);

            string targetPath = EditorUtility.SaveFilePanel("保存位置", Application.streamingAssetsPath, "CombinedAssets.assetbundle", "assetbundle");
            if (string.IsNullOrEmpty(targetPath))
            {
                Debug.Log("取消打包操作");
                return;
            }
            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
#if     UNITY_ANDROID
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android))
#elif   UNITY_IPHONE
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iPhone))
#elif   UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows))
#else
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.UncompressedAssetBundle))
#endif
            {
                for (int i = 0, iMax = selectedAssets.Length; i < iMax; i++)
                {
                    Debug.Log(string.Format("Create AssetBundles {0}", selectedAssets[i].name));
                }
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Assets/GF47 Editor/AssetBundles/Create Combined Compressed")]
        private static void CreateCombinedCompressedAssetBundles()
        {
            Caching.CleanCache();

            if (!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);

            string targetPath = EditorUtility.SaveFilePanel("保存位置", Application.streamingAssetsPath, "CombinedAssets.assetbundle", "assetbundle");
            if (string.IsNullOrEmpty(targetPath))
            {
                Debug.Log("取消打包操作");
                return;
            }
            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
#if     UNITY_ANDROID
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android))
#elif   UNITY_IPHONE
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone))
#elif   UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows))
#else
            if (BuildPipeline.BuildAssetBundle(null, selectedAssets, targetPath, BuildAssetBundleOptions.CollectDependencies))
#endif
            {
                for (int i = 0, iMax = selectedAssets.Length; i < iMax; i++)
                {
                    Debug.Log(string.Format("Create AssetBundles {0}", selectedAssets[i].name));
                }
            }
            AssetDatabase.Refresh();
        }
    }
}
