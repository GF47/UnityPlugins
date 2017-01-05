/* ****************************************************************
 * @File Name   :   SceneTools
 * @Author      :   GF47
 * @Date        :   2015/10/13 12:52:09
 * @Description :   to do
 * @Edit        :   2015/10/13 12:52:09
 * ***************************************************************/

using UnityEditor;
using UnityEngine;

namespace GF47Editor.Editor
{
    /// <summary>
    /// A example class in Unity
    /// </summary>
    public class SceneTools : ScriptableObject
    {
        [MenuItem("Tools/GF47 Editor/转到顶视图 %t", false, 1)]
        private static void Turn2TopView()
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                view.LookAt(view.pivot, Quaternion.Euler(90f, 0f, 0f), 0.25f, true);
            }
        }

        [MenuItem("Tools/GF47 Editor/转到模型顶视图 %&t", false, 1)]
        private static void Turn2LocalView()
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                Transform t = Selection.activeTransform;
                if (t != null)
                {
                    view.LookAt(t.position, t.rotation * Quaternion.Euler(90f, 0f, 0f), 0.25f, true);
                }
            }
        }
    }
}
