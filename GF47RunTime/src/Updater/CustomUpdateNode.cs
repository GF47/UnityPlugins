namespace GF47RunTime.Updater
{
    using System;

    public class CustomUpdateNode : IUpdateNode
    {
        public bool IsUpdating
        {
            get { return isUpdating; }
            set
            {
                if (isUpdating != value)
                {
                    isUpdating = value;
                    if (isUpdating)
                    {
                        Start();
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
        }
        protected bool isUpdating;

        public float Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        private float _duration;
        private float _timeMeter;

        public event Action<float> OnUpdate;

        public CustomUpdateNode(Action<float> callback, float duration)
        {
            OnUpdate = callback;
            _duration = duration < 0.02f ? 1f : duration;
        }

        public CustomUpdateNode(Action<float> callback) : this(callback, 1f) { }

        public void Update(float delta)
        {
            _timeMeter += delta;
            if (_timeMeter > _duration)
            {
                if (OnUpdate != null)
                {
                    OnUpdate(_duration);
                }
                _timeMeter = 0f;
            }
        }

        public void Start()
        {
            MonoUpdater.Instance.Target.Add(this, Updater.UpdateStyle.PerCustomFrame);
            isUpdating = true;
        }

        public void Stop()
        {
            MonoUpdater.Instance.Target.RemoveFrom(this, Updater.UpdateStyle.PerCustomFrame);
            isUpdating = false;
        }

        public void Clear()
        {
            Stop();
            OnUpdate = null;
        }

        #region Dispose
        private bool _isDisposed;
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // 托管资源
                    OnUpdate = null;
                    // DONE 从[Updater]中删除自身
                    Stop();
                }
                // 非托管资源
                _isDisposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CustomUpdateNode()
        {
            Dispose(false);
        }
        #endregion
    }
}
