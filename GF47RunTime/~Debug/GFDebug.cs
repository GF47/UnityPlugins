/***************************************************************
 * @File Name       : GFDebug
 * @Author          : GF47
 * @Description     : 一个简单的Log
 * @Date            : 2017/5/16/星期二 10:11:56
 * @Edit            : none
 **************************************************************/

using System;
using System.Collections;
using UnityEngine;

namespace GF47RunTime
{
    public class GFDebug : MonoBehaviour
    {
        public static Vector2 Size = new Vector2(512f, 40f);
        public static Vector2 Pos = new Vector2(4f, 4f);

        private static readonly GFDebug Instance;

        static GFDebug()
        {
            if (Instance == null)
            {
                GameObject go = new GameObject("__GFDebug")
                {
                    hideFlags = HideFlags.HideInHierarchy,
                };
                DontDestroyOnLoad(go);
                Instance = go.AddComponent<GFDebug>();
                _infos = new string[4];
            }
        }

        private static string[] _infos;
        public static int Capacity
        {
            get { return _infos.Length; }
            set
            {
                if (value == 0) { return; }
                if (value == _infos.Length) { return; }

                Array.Resize(ref _infos, value);
            }
        }

        public static int Count = 4;

        private static int _current;
        private static int GetPrevious(int index)
        {
            index--;
            if (index < 0)
            {
                index += Capacity;
            }
            return index;
        }

        public static bool ShowLog { get { return Instance.enabled; } set { Instance.enabled = value; } }

        public static void Log(object o, float time)
        {
            Log(o);
            Instance.Execute(time, () => Instance.enabled = false);
        }

        public static void Log(object o)
        {
            _current++;
            if (_current >= Capacity)
            {
                _current = 0;
            }

            _infos[_current] = o.ToString();
            Instance.enabled = true;
        }

        void OnGUI()
        {
            Rect r = new Rect(Pos.x, Pos.y, Size.x, Size.y);
            for (int i = 0, cursor = _current; i < Count; i++, cursor = GetPrevious(cursor), r = new Rect(r.x, r.y + Size.y, r.width, r.height))
            {
                if (!string.IsNullOrEmpty(_infos[cursor])) { GUI.Label(r, _infos[cursor]); }
            }
        }

        private void Execute(float time, Action action)
        {
            StartCoroutine(_timer(time, action));
        }

        private IEnumerator _timer(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        // private class CircluarlyLinkedList<T>
        // {
        //     public class Node<T>
        //     {
        //         public Node<T> Next { get; set; }
        //         public T Value { get; set; }
        //     }

        //     public int CurrentIndex => _currentIndex;
        //     private int _currentIndex;

        //     public Node<T> Head { get; set; }
        //     public Node<T> Rear { get; set; }

        //     public CircluarlyLinkedList()
        //     {
        //         Head = new Node<T>();
        //         Rear = Head;
        //     }
        // }
    }
}
