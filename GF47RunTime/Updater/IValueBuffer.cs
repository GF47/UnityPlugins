using System;

namespace GF47RunTime.Updater
{
    public interface IValueBuffer<T> where T : struct 
    {
        event Action<T> OnValueChangeHandler;
        bool State { get; set; }
        T Target { get; set; }
        T Current { get; set; }
        float Duration { get; set; }
        float Percent { get; }

        void Clear();
    }
}
