/* ****************************************************************
 * @File Name   :   Updater
 * @Author      :   GF47
 * @Date        :   2015/8/6 9:27:36
 * @Description :   用于逐帧调用某个方法，以及取得未经[Unity TimeScale]缩放过的时间
 * @Edit        :   2015/8/6 9:27:36
 * ***************************************************************/

namespace GF47RunTime.Updater
{
    using UnityEngine;

    /// <summary>
    /// 用于逐帧调用某个方法，以及取得未经[Unity TimeScale]缩放过的时间
    /// </summary>
    public class MonoUpdater : MonoCarrier<Updater>
    {
        public static MonoUpdater Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_isApplicationRunning)
                    {
                        GameObject instance = new GameObject("__MonoUpdater");
                        instance.hideFlags = HideFlags.HideInHierarchy;
                        DontDestroyOnLoad(instance);

                        _instance = instance.AddComponent<MonoUpdater>();
                    }
                }
                return _instance;
            }
        }
        private static MonoUpdater _instance;
        private static bool _isApplicationRunning = true;

        public static float RealTime
        {
            get { return Instance.Target.RealTime; }
        }

        public static float RealDelta
        {
            get { return Instance.Target.RealDeltaTime; }
        }

        public static float RealLateTime
        {
            get { return Instance.Target.RealLateTime; }
        }

        public static float RealLateDelta
        {
            get { return Instance.Target.RealLateDeltaTime; }
        }

        public static float CustomTime
        {
            get { return Instance.Target.CustomTime; }
            set { Instance.Target.CustomTime = value; }
        }

        public override Updater Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private Updater _target;

        void Awake()
        {
            _target = new Updater();
        }

        void Update()
        {
            _target.Update();
        }

        void FixedUpdate()
        {
            _target.FixedUpdate();
        }

        void LateUpdate()
        {
            _target.LateUpdate();
        }

        void OnDestroy()
        {
            _isApplicationRunning = false;
        }
    }
}