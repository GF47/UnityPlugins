//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/5 23:21:11
//      Edited      :       2013/9/5 23:21:11
//************************************************************//

namespace GF47RunTime
{
    using System;
    using System.IO;
    using UnityEngine;
    using System.Reflection;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/5 23:21:11
    /// GF47ToolIntroduction    :nothing to say
    /// </summary>
    public static class Toolkit
    {
        /// <summary> 将 target(GameObject) 的状态设置为 value
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="value">状态</param>
        public static void SetGameObjectActivate(GameObject target, bool value)
        {
            if (target == null)
            {
                return;
            }
            if (target.activeSelf ^ value)
            {
                target.SetActive(value);
            }
        }

        /// <summary> 将 target(Collider) 的状态设置为 value
        /// </summary>
        /// <param name="target">目标碰撞体</param>
        /// <param name="value">状态</param>
        public static void SetColliderEnable(Collider target, bool value)
        {
            if (target == null)
            {
                return;
            }
            if (target.enabled ^ value)
            {
                target.enabled = value;
            }
        }

        /// <summary> 将 target(Render) 的状态设置为 value
        /// </summary>
        /// <param name="target">目标渲染组件</param>
        /// <param name="value">状态</param>
        public static void SetMeshRenderEnable(Renderer target, bool value)
        {
            if (target == null)
            {
                return;
            }
            if (target.enabled ^ value)
            {
                target.enabled = value;
            }
        }

        /// <summary> 在整形数 min 和 max 之间取一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int RandomRange(int min, int max)
        {
            if (min == max)
            {
                return min;
            }
            return UnityEngine.Random.Range(min, max + 1);
        }

        /// <summary> 在浮点数 min 和 max 之间取一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float RandomRange(float min, float max)
        {
            if (Math.Abs(min - max) < 1e-6f)
            {
                return min;
            }
            return UnityEngine.Random.Range(min, max);
        }

        /// <summary> 返回目标物体的完整层级
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetHierarchy(GameObject obj)
        {
            string path = obj.name;

            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = string.Format(@"{0}\{1}", obj.name, path);
            }
            return string.Format(@"\{0}\", path);
        }

        /// <summary> 在场景中查找带有 T 组件的物体
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>T 类型的数组</returns>
        public static T[] FindInScene<T>() where T : Component
        {
            return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
        }

        /// <summary> 在目标物体上添加一个子物体
        /// </summary>
        /// <param name="parent">目标物体</param>
        /// <returns>子物体</returns>
        public static GameObject AddChild(GameObject parent)
        {
            GameObject go = new GameObject();

            if (parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.layer;
            }
            return go;
        }

        /// <summary> 在目标物体上添加一个实例物体的复制作为子物体
        /// </summary>
        /// <param name="parent">目标物体</param>
        /// <param name="prefab">实例物体</param>
        /// <returns>子物体</returns>
        public static GameObject AddChild(GameObject parent, GameObject prefab)
        {
            GameObject go = UnityEngine.Object.Instantiate(prefab);

            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.layer;
            }
            return go;
        }

        /// <summary> 在目标物体上添加一个带有 T 组件的物体作为子物体
        /// </summary>
        /// <typeparam name="T">组件类</typeparam>
        /// <param name="parent">目标物体</param>
        /// <returns>子物体的T组件实例</returns>
        public static T AddChild<T>(GameObject parent) where T : Component
        {
            GameObject go = AddChild(parent);
            go.name = typeof(T).ToString();
            return go.AddComponent<T>();
        }

        /// <summary> 在父层级上查找离目标物体关系最近的 T 组件
        /// </summary>
        /// <typeparam name="T">组件类</typeparam>
        /// <param name="target">目标物体</param>
        /// <returns>查找到的 T 组件</returns>
        public static T FindInParent<T>(GameObject target) where T : Component
        {
            if (target == null) return null;
            T component = target.GetComponent<T>();

            if (component == null)
            {
                Transform t = target.transform.parent;

                while (t != null && component == null)
                {
                    component = t.gameObject.GetComponent<T>();
                    t = t.parent;
                }
            }
            return component;
        }

        /// <summary> 销毁物体
        /// </summary>
        /// <param name="target">将要被销毁的物体</param>
        public static void Destroy(UnityEngine.Object target)
        {
            if (target != null)
            {
                if (Application.isPlaying)
                {
                    if (target is GameObject)
                    {
                        GameObject go = target as GameObject;
                        go.transform.parent = null;
                    }
                    UnityEngine.Object.Destroy(target);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(target);
                }
            }
        }

        /// <summary> 判断 child 是否是 parent 的子物体
        /// </summary>
        /// <param name="parent">假定的父物体</param>
        /// <param name="child">假定的子物体</param>
        /// <returns>是否是子物体</returns>
        public static bool IsChild(Transform parent, Transform child)
        {
            if (parent == null || child == null) return false;

            while (child != null)
            {
                if (child == parent) return true;
                child = child.parent;
            }
            return false;
        }

        /// <summary> 设置物体的层级，包括子物体
        /// </summary>
        /// <param name="target">目标物体</param>
        /// <param name="layer">层级</param>
        public static void Setlayer(GameObject target, int layer)
        {
            target.layer = layer;

            Transform t = target.transform;
            for (int i = 0, iMax = t.childCount; i < iMax; ++i)
            {
                Transform child = t.GetChild(i);
                Setlayer(child.gameObject, layer);
            }
        }

        /// <summary>返回一个向量的整数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 Round(Vector3 value)
        {
            value.x = Mathf.Round(value.x);
            value.y = Mathf.Round(value.y);
            value.z = Mathf.Round(value.z);
            return value;
        }

        /// <summary> 磁盘是否可以写入
        /// </summary>
        public static bool FileAccess
        {
            get
            {
                return Application.platform != RuntimePlatform.WindowsWebPlayer &&
                       Application.platform != RuntimePlatform.OSXWebPlayer;
            }
        }

        /// <summary> 将二进制数据写入文件，储存在Unity3D的持久化存储路径中
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="bytes">二进制数据</param>
        /// <returns>写入成功</returns>
        public static bool SaveAsFile(string fileName, byte[] bytes)
        {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO
		return false;
#else
            if (!FileAccess) return false;

            string path = Application.persistentDataPath + "/" + fileName;

            if (bytes == null)
            {
                if (File.Exists(path)) File.Delete(path);
                return true;
            }

            FileStream file;

            try
            {
                file = File.Create(path);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                return false;
            }

            file.Write(bytes, 0, bytes.Length);
            file.Close();
            return true;
#endif
        }

        /// <summary> 从Unity3D的持久化存储路径中读取名为 fileName 的文件，并以二进制数据的形式返回文件中的数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>二进制数据</returns>
        public static byte[] LoadFromFile(string fileName)
        {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO
		return null;
#else
            if (!FileAccess) return null;

            string path = Application.persistentDataPath + "/" + fileName;

            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
#endif
        }

        /// <summary>
        /// 将整形数值限制在阈值内
        /// </summary>
        public static int Clamp(int t, int min, int max)
        {
            if (t >= max)
            {
                return max;
            }
            if (t <= min)
            {
                return min;
            }
            return t;
        }

        /// <summary>
        /// 将浮点型数值限制在阈值内
        /// </summary>
        public static float Clamp(float t, float min, float max)
        {
            if (t >= max)
            {
                return max;
            }
            if (t <= min)
            {
                return min;
            }
            return t;
        }

        public static T GetInterface<T>(GameObject target) where T : class
        {
            MonoBehaviour[] scripts = target.GetComponents<MonoBehaviour>();
            if (scripts != null)
            {
                for (int i = 0; i < scripts.Length; i++)
                {
                    if (scripts[i] is T)
                    {
                        return scripts[i] as T;
                    }
                }
            }
            return null;
        }

        private const BindingFlags NonPublicBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// 获取一个实例中的属性值或字段值
        /// </summary>
        /// <param name="obj">具体实例</param>
        /// <param name="name">属性名或字段名</param>
        /// <param name="flags">属性或字段的筛选标志</param>
        /// <returns></returns>
        public static object GetPropertyOrFieldValue(object obj, string name, BindingFlags flags = NonPublicBindingFlags)
        {
            Type objType = obj.GetType();

            FieldInfo fInfo = objType.GetField(name, flags);
            if (fInfo != null)
            {
                return fInfo.GetValue(obj);
            }

            PropertyInfo pInfo = objType.GetProperty(name, flags);
            if (pInfo != null)
            {
                return pInfo.GetValue(obj, null);
            }

            return null;
        }

        public static bool SetPropertyOrFieldValue(object obj, string name, object value, BindingFlags flags = NonPublicBindingFlags)
        {
            Type objType = obj.GetType();

            FieldInfo fInfo = objType.GetField(name, flags);
            if (fInfo != null)
            {
#if DEBUG
                try
                {
#endif
                    fInfo.SetValue(obj, value);
#if DEBUG
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
#endif
                return true;
            }

            PropertyInfo pInfo = objType.GetProperty(name, flags);
            if (pInfo != null)
            {
#if DEBUG
                try
                {
#endif
                    pInfo.SetValue(obj, value, null);
#if DEBUG
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
#endif
                return true;
            }

            return false;
        }

        public static bool GetCurrentCollisionAtMousePosition(Camera camera, out RaycastHit hit, float distance = -1f)
        {
            if (camera != null)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                float d = distance > 0f ? distance : camera.farClipPlane;
                if (Physics.Raycast(ray, out hit, d, camera.cullingMask))
                {
                    return true;
                }
            }
            hit = new RaycastHit();
            return false;
        }
    }
}