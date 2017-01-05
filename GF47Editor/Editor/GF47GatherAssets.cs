using UnityEditor;
using UnityEngine;

namespace GF47Editor.Editor
{
    public class GF47GatherAssets : ScriptableObject
    {
        [MenuItem("Assets/GF47 Editor/Gather Asset To &g", false , 0)]
        static void GatherAssets()
        {
            Caching.CleanCache();

            Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.TopLevel);

            string targetPath = EditorUtility.OpenFolderPanel("复制到", Application.dataPath, string.Empty);
            if (string.IsNullOrEmpty(targetPath))
            {
                Debug.Log("取消复制操作");
                return;
            }
            for (int i = 0; i < selectedAssets.Length; i++)
            {
                if (AssetDatabase.Contains(selectedAssets[i]))
                {
                    string path = AssetDatabase.GetAssetPath(selectedAssets[i]);
                    if (AssetDatabase.CopyAsset(path, targetPath))
                    {
                        string metaDataPath = AssetDatabase.GetTextMetaFilePathFromAssetPath(path);
                        string metaDataName = metaDataPath.Substring(metaDataPath.LastIndexOf('/') + 1);
                        if (!string.IsNullOrEmpty(metaDataPath))
                        {
                            FileUtil.CopyFileOrDirectory(metaDataPath, string.Format("{0}/{1}", targetPath,metaDataName));
                        }
                    }
                }
            }
        }
    }
}
