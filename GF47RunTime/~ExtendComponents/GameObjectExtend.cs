using System;

namespace GF47RunTime
{
    using UnityEngine;

    public static class GameObjectExtend
    {
        /// <summary> ����Ŀ������������㼶
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetHierarchy(this GameObject obj)
        {
            string path = obj.name;

            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = string.Format(@"{0}\{1}", obj.name, path);
            }
            return string.Format(@"\{0}\", path);
        }

        /// <summary> ��Ŀ�����������һ��������
        /// </summary>
        /// <param name="parent">Ŀ������</param>
        /// <returns>������</returns>
        public static GameObject AddChild(this GameObject parent)
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

        /// <summary> ��Ŀ�����������һ��ʵ������ĸ�����Ϊ������
        /// </summary>
        /// <param name="parent">Ŀ������</param>
        /// <param name="prefab">ʵ������</param>
        /// <returns>������</returns>
        public static GameObject AddChild(this GameObject parent, GameObject prefab)
        {
            GameObject go = Object.Instantiate(prefab);

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

        /// <summary> ��Ŀ�����������һ������ T �����������Ϊ������
        /// </summary>
        /// <typeparam name="T">�����</typeparam>
        /// <param name="parent">Ŀ������</param>
        /// <returns>�������T���ʵ��</returns>
        public static T AddChild<T>(this GameObject parent) where T : Component
        {
            GameObject go = AddChild(parent);
            go.name = typeof(T).ToString();
            return go.AddComponent<T>();
        }

        /// <summary> �ڸ��㼶�ϲ�����Ŀ�������ϵ����� T ���
        /// </summary>
        /// <typeparam name="T">�����</typeparam>
        /// <param name="target">Ŀ������</param>
        /// <returns>���ҵ��� T ���</returns>
        public static T FindInParent<T>(this GameObject target) where T : Component
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

        /// <summary> ��������Ĳ㼶������������
        /// </summary>
        /// <param name="target">Ŀ������</param>
        /// <param name="layer">�㼶</param>
        public static void Setlayer(this GameObject target, int layer)
        {
            target.layer = layer;

            Transform t = target.transform;
            for (int i = 0, iMax = t.childCount; i < iMax; ++i)
            {
                Transform child = t.GetChild(i);
                Setlayer(child.gameObject, layer);
            }
        }

        public static T GetInterface<T>(this GameObject target) where T : class
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
        public static T[] GetInterfaces<T>(this GameObject target) where T : class
        {
            MonoBehaviour[] scripts = target.GetComponents<MonoBehaviour>();
            if (scripts != null)
            {
                T[] results = new T[scripts.Length];
                int n = 0;
                for (int i = 0; i < scripts.Length; i++)
                {
                    T tmp = scripts[i] as T;
                    if (tmp != null)
                    {
                        results[n] = tmp;
                        n++;
                    }
                }
                Array.Resize(ref results, n);
                return results;
            }
            return null;
        }
    }
}