/* ****************************************************************
 * @File Name   :   GF47GetRelativeTransform
 * @Author      :   GF47
 * @Date        :   2015/8/27 14:20:50
 * @Description :   to do
 * @Edit        :   2015/8/27 14:20:50
 * ***************************************************************/

using UnityEditor;
using UnityEngine;
using System.Text;

namespace GF47Editor.Editor
{
    /// <summary>
    /// A example class in Unity
    /// </summary>
    public class GF47GetRelativeTransform : EditorWindow
    {
        public Transform father;
        public Transform child;

        [MenuItem("Tools/GF47 Editor/Transform/Get Relative Transform")]
        static void Init()
        {
            GF47GetRelativeTransform window = GetWindow<GF47GetRelativeTransform>();
            window.position = new Rect(200f, 200f, 400f, 400f);
            window.Show();
        }

        void OnGUI()
        {
            father = EditorGUILayout.ObjectField("父物体", father, typeof(Transform), true) as Transform;
            child = EditorGUILayout.ObjectField("子物体", child, typeof(Transform), true) as Transform;
            if (GUILayout.Button("获取"))
            {
                if (father != null && child != null)
                {
                    Matrix4x4 world2FatherMatrix = father.worldToLocalMatrix;
                    Vector3 pos = world2FatherMatrix.MultiplyPoint(child.position);
                    Vector3 eulerAngles = (Quaternion.Inverse(father.rotation) * child.rotation).eulerAngles;
                    Vector3 scale = world2FatherMatrix.MultiplyVector(child.lossyScale);

                    StringBuilder tmp = new StringBuilder();
                    tmp.Append("<position>");
                    tmp.Append(pos.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(pos.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(pos.z.ToString("F4"));
                    tmp.Append("</position>\n");

                    tmp.Append("<eulerAngles>");
                    tmp.Append(eulerAngles.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(eulerAngles.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(eulerAngles.z.ToString("F4"));
                    tmp.Append("</eulerAngles>\n");

                    tmp.Append("<scale>");
                    tmp.Append(scale.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(scale.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(scale.z.ToString("F4"));
                    tmp.Append("</scale>\n");

                    EditorGUIUtility.systemCopyBuffer = tmp.ToString();
                    Debug.Log(string.Format("子物体{0}相对父物体{1}的Transform为:", child, father));
                    Debug.Log(tmp);
                }
                else if(child != null)
                {
                    Vector3 pos = child.position;
                    Vector3 eulerAngles = child.eulerAngles;
                    Vector3 scale = child.lossyScale;

                    StringBuilder tmp = new StringBuilder();
                    tmp.Append("<position>");
                    tmp.Append(pos.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(pos.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(pos.z.ToString("F4"));
                    tmp.Append("</position>\n");

                    tmp.Append("<eulerAngles>");
                    tmp.Append(eulerAngles.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(eulerAngles.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(eulerAngles.z.ToString("F4"));
                    tmp.Append("</eulerAngles>\n");

                    tmp.Append("<scale>");
                    tmp.Append(scale.x.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(scale.y.ToString("F4"));
                    tmp.Append(",");
                    tmp.Append(scale.z.ToString("F4"));
                    tmp.Append("</scale>\n");

                    EditorGUIUtility.systemCopyBuffer = tmp.ToString();
                    Debug.Log(string.Format("物体{0}的Transform为:", child));
                    Debug.Log(tmp);
                }
            }
        }
    }
}