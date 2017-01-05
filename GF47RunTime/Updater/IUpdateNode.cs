namespace GF47RunTime.Updater
{
    using System;

    public interface IUpdateNode : IDisposable
    {
        bool IsUpdating { get; set; }
        event Action<float> OnUpdate;

        void Update(float delta);

        void Start();
        void Stop();

        void Clear();
    }
}