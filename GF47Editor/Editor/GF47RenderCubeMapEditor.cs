using UnityEditor;
using UnityEngine;

namespace GF47Editor.Editor
{
    public class GF47RenderCubeMapEditor : EditorWindow
    {
        public Transform renderFromPosition;
        public Cubemap cubeMap;
        public Material theSkyBoxMaterial;

        [MenuItem("Tools/GF47 Editor/Render/Render Into CubeMap")]
        static void Init()
        {
            GF47RenderCubeMapEditor window = GetWindow<GF47RenderCubeMapEditor>();
            window.position = new Rect(200, 200, 400, 400);
            window.Show();
        }

        void OnGUI()
        {
            renderFromPosition = EditorGUILayout.ObjectField("相机位置",renderFromPosition, typeof(Transform),true) as Transform;
            cubeMap = EditorGUILayout.ObjectField("立方体环境贴图",cubeMap, typeof(Cubemap), true) as Cubemap;
            theSkyBoxMaterial = EditorGUILayout.ObjectField("天空球材质",theSkyBoxMaterial, typeof(Material), true) as Material;
            if (renderFromPosition != null && cubeMap != null)
            {
                if (GUILayout.Button(new GUIContent("Render", "click to render the cubemap at the transform's position")))
                {
                    GameObject go = new GameObject("_CubeMapCamera", typeof(Camera));
                    go.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
	
                    if (!go.GetComponent<Skybox>())
                    {
                        go.AddComponent<Skybox>();
                    }
	
                    go.GetComponent<Skybox>().material = theSkyBoxMaterial;
	
                    go.transform.position = renderFromPosition.position;
                    if (renderFromPosition.GetComponent<Renderer>())
                    {
                        go.transform.position = renderFromPosition.GetComponent<Renderer>().bounds.center;
                    }
	
                    go.transform.rotation = Quaternion.identity;
	
                    go.GetComponent<Camera>().fieldOfView = 90f;
                    go.GetComponent<Camera>().aspect = 1f;
	
                    go.GetComponent<Camera>().RenderToCubemap(cubeMap);
	
                    DestroyImmediate(go);
                }
            }
        }
    }
}
