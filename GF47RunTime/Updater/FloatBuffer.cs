using System;

namespace GF47RunTime.Updater
{
    public class FloatBuffer : IValueBuffer<float>
    {
        private bool _state;
        private float _value;
        private float _initialValue;
        private float _differenceValue;
        private float _duration;
        private float _percent;

        private PerFrameUpdateNode _updaterNode;

        public event Action<float> OnValueChangeHandler;
        public event Action<float> OnValueChangeStartHandler;
        public event Action<float> OnValueChangeStopHandler;

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

        public float Target
        {
            get { return _initialValue + _differenceValue; }
            set
            {
                _initialValue = _value;
                _differenceValue = value - _value;
                _percent = 0f;
                State = Math.Abs(_differenceValue) > 1e-6f;
            }
        }

        public float Current
        {
            get { return _value; }
            set
            {
                _value = value;
                _percent = (_value - _initialValue) / _differenceValue;
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

        public FloatBuffer(float initialValue , Action<float> callback, float duration)
        {
            OnValueChangeHandler = callback;
            Current = initialValue;
            Duration = duration;
        }
        public FloatBuffer(float initialValue, Action<float> callback) : this(initialValue, callback, 1f) { }

        public void Update(float delta)
        {
            _percent += delta / _duration;
            if (_percent >= 1f)
            {
                State = false;
            }
            _percent = Math.Min(_percent, 1f);
            _value = _initialValue + _percent * _differenceValue;
            if (OnValueChangeHandler != null)
            {
                OnValueChangeHandler(_value);
            }
        }

        public void Clear()
        {
            OnValueChangeHandler = null;
            OnValueChangeStartHandler = null;
            OnValueChangeStopHandler = null;
        }
    }
}
