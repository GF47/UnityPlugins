/***************************************************************
 * @File Name       : MonoSingleton
 * @Author          : GF47
 * @Description     : 单例
 * @Date            : 2017/7/17/星期一 11:51:07
 * @Edit            : none
 **************************************************************/

using UnityEngine;

namespace GF47RunTime
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }
            GameObject go = _instance.gameObject;
            Destroy(go);
        }
    }
    public class Singleton<T> where T : class , new()
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) { _instance =new T(); }
                return _instance;
            }
        }
    }

}
