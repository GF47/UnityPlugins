using System;

namespace GF47RunTime.Updater
{
    public interface IValueBuffer<T> where T : struct 
    {
        Action<T> OnValueChangeHandler { get; set; }
        Action<T> OnValueChangeStartHandler { get; set; }
        Action<T> OnValueChangeStopHandler { get; set; }
        bool State { get; set; }
        T Target { get; set; }
        T Current { get; set; }
        float Duration { get; set; }
        float Percent { get; }

        void Clear();
    }
}
