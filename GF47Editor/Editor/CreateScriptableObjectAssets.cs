﻿using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

public class CreateScriptableObjectAssets : EditorWindow
{
    [MenuItem("Tools/GF47 Editor/创建一个指定类型的AssetData")]
    private static void Init()
    {
        CreateScriptableObjectAssets window = GetWindow<CreateScriptableObjectAssets>();
        window.Show();
    }

    private Object _data;
    private ScriptableObject _scriptableObjectData;

    void OnGUI()
    {
        _data = EditorGUILayout.ObjectField("类型", _data, typeof(MonoScript), false);
        MonoScript monoScript = _data as MonoScript;
        if (monoScript != null)
        {
            Type type = monoScript.GetClass();
            if (GUILayout.Button("创建"))
            {
                try
                {
                    _scriptableObjectData = CreateInstance(type);
                    string path = EditorUtility.SaveFilePanelInProject("选择保存位置", type.Name, "asset", "message");
                    AssetDatabase.CreateAsset(_scriptableObjectData, path);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}
