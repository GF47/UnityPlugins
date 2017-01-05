namespace GF47RunTime.Updater
{
    using System;

    public abstract class NormalUpdateNode : IUpdateNode
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

        public event Action<float> OnUpdate;

        public NormalUpdateNode(Action<float> callback)
        {
            OnUpdate = callback;
        }

        public void Update(float delta)
        {
            if (OnUpdate != null)
            {
                OnUpdate(delta);
            }
        }

        public abstract void Start();

        public abstract void Stop();
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
        ~NormalUpdateNode()
        {
            Dispose(false);
        }
        #endregion
    }
}