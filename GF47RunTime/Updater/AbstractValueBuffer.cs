/***************************************************************
 * @File Name       : AbstractValueBuffer
 * @Author          : GF47
 * @Description     : 值类型Buffer的基类
 * @Date            : 2017/8/16/星期三 10:22:46
 * @Edit            : none
 **************************************************************/

using System;

namespace GF47RunTime.Updater
{
    public abstract class AbstractValueBuffer<T>
        : IValueBuffer<T>
        where T : struct
    {
        private bool _state;
        private T _value;
        private T _initialValue;
        private T _differenceValue;
        private float _duration;
        private float _percent;

        private PerFrameUpdateNode _updaterNode;

        public Action<T> OnValueChangeHandler { get; set; }
        public Action<T> OnValueChangeStartHandler { get; set; }
        public Action<T> OnValueChangeStopHandler { get; set; }

        public bool State
        {
            get { return _state; }
            set
            {
                if (_state == value) { return; }

                if (_updaterNode == null)
                {
                    _updaterNode = new PerFrameUpdateNode(Update);
                }

                _state = value;

                if (_state)
                {
                    _updaterNode.Start();
                    if (OnValueChangeStartHandler != null) { OnValueChangeStartHandler(_value); }
                }
                else
                {
                    _updaterNode.Stop();
                    if (OnValueChangeStopHandler != null) { OnValueChangeStopHandler(_value); }
                }
            }
        }

        public T Target
        {
            get { return Addition(_initialValue, _differenceValue); }
            set
            {
                _initialValue = _value;
                _differenceValue = Subtraction(value, _initialValue);
                _percent = 0f;
                State = IsLengthGreaterThanTMin(_differenceValue);
            }
        }

        public T Current
        {
            get { return _value; }
            set
            {
                _value = value;
                T targetValue = Addition(_initialValue, _differenceValue);

                _percent = Projection(_value, _initialValue, targetValue);

                T result;
                if (Division(Subtraction(targetValue, _value), 1f - _percent, out result))
                {
                    _differenceValue = result;
                }
            }
        }

        public float Duration
        {
            get { return _duration; }
            set { _duration = Math.Max(value, 0.01f); }
        }

        public float Percent
        {
            get { return _percent; }
        }


        protected AbstractValueBuffer(T initialValue , Action<T> callback, float duration)
        {
            OnValueChangeHandler = callback;
            Current = initialValue;
            Duration = duration;
        }
        protected AbstractValueBuffer(T initialValue, Action<T> callback) : this(initialValue, callback, 1f) { }

        public void Update(float delta)
        {
            _percent += delta / _duration;
            float tmp = _percent;
            _percent = Math.Min(_percent, 1f);

            _value = Addition(_initialValue, Multiplication(_percent, _differenceValue));
            if (OnValueChangeHandler != null) { OnValueChangeHandler(_value); }

            if (tmp >= 1f) { State = false; }
        }

        public void Clear()
        {
            OnValueChangeHandler = null;
            OnValueChangeStartHandler = null;
            OnValueChangeStopHandler = null;
        }

        protected abstract T Addition(T a, T b);
        protected abstract T Subtraction(T a, T b);
        protected abstract T Multiplication(float multiplier, T originValue);
        protected abstract bool Division(T originValue, float divisor, out T result);
        protected abstract bool IsLengthGreaterThanTMin(T value);
        protected virtual float Projection(T value, T start, T end) { return 0f; }
    }
}
