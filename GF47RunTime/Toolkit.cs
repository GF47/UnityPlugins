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
        /// <summary> 在场景中查找带有 T 组件的物体
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>T 类型的数组</returns>
        public static T[] FindInScene<T>() where T : Component
        {
            return UnityEngine.Object.FindObjectsOfType(typeof(T)) as T[];
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
    }
}