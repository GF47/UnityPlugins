﻿/***************************************************************
 * @File Name       : MonoSingleton
 * @Author          : GF47
 * @Description     : 单例
 * @Date            : 2017/7/17/星期一 11:51:07
 * @Edit            : none
 **************************************************************/

using System;
using UnityEngine;

namespace GF47RunTime
{
    public class Singleton<T> 
    {
        public static T Instance
        {
            get
            {
                if (instance == null) { instance = ConstructFunc(); }
                return instance;
            }
        }
        protected static T instance;

        public static Func<T> ConstructFunc;
    }

    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (instance == null) { instance = ConstructFunc(); }
                return instance;
            }
        }

        protected static T instance;

        public static Func<T> ConstructFunc;
    }
}
