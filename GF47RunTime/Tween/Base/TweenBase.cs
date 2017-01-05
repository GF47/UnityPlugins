//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/3/2 2:44:32
//      Edited      :       2014/3/2 2:44:32
//************************************************************//

using System;
using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author              :GF47
    /// DataTime            :2014/3/2 2:44:32
    /// [TweenBase]    Introduction    :缓动基类
    /// </summary>
    public abstract class TweenBase : MonoBehaviour
    {
        public const int PublicGroup = 0;

        public Action<TweenBase> OnFinished;

        public TweenEase easeType = TweenEase.Linear;
        public TweenLoop loopType = TweenLoop.Once;

        public bool realTime = true;

        public float delay = 0.0f;

        public float duration = 1.0f;

        public int tweenGroup = 1;

        public GameObject eventReceiver;
        public string callWhenFinished;

        private bool _started;
        private float _startTime;
        private float _duration;
        private float _amountPerDelta = 1.0f;
        private float _factor;
        private Factor _result;

        private EaseAlgorithm _ease;
        private DirectionAlgorithm _direction;
        private LoopAlgorithm _loop;

        void Start()
        {
            ResetAlgorithm(easeType, loopType, TweenDirection.Forward);
            Update();
        }

        void Update()
        {
            float delta = realTime ? Updater.MonoUpdater.RealDelta : Time.deltaTime;
            float time = realTime ? Updater.MonoUpdater.RealTime : Time.time;

            if (!_started)
            {
                _started = true;
                _startTime = time + delay;
            }
            if (time < _startTime) return;

            _factor += GetAmountPerDelta() * delta;
            _result = _loop.Result(_factor);
            _result.factor = _direction.Result(_result.factor);
            _result.factor = _ease.Result(_result.factor);

            _factor %= 1.0f;

            Sample(_result.factor, _result.isFinished);
        }

        protected void Sample(float factor, bool finished)
        {
            SetPercent(factor, finished);
            if (finished)
            {
                OnFinishedBehaviour();
                _result.isFinished = false;
                enabled = false;
            }
        }

        protected void OnFinishedBehaviour()
        {
            if (OnFinished != null) OnFinished(this);
            if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
            {
                eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
            }
        }

        protected float GetAmountPerDelta()
        {
            if (Math.Abs(_duration - duration) > 0.0001f)
            {
                _duration = duration;
                _amountPerDelta = (_duration > 0 ? (1.0f / _duration) : 1000f);
            }
            return _amountPerDelta;
        }

        public abstract void SetPercent(float factor, bool isFinished);

        public float GetPercent()
        {
            return _result.factor;
        }

        public void ResetAlgorithm(TweenEase ease, TweenLoop loop, TweenDirection dir)
        {
            if (_ease == null)
            {
                _ease = new EaseAlgorithm(ease);
            }
            else _ease.EaseType = ease;
            if (_loop == null)
            {
                _loop = new LoopAlgorithm(loop);
            }
            else _loop.LoopType = loop;
            if (_direction == null)
            {
                _direction = new DirectionAlgorithm(dir);
            }
            else _direction.DirectionType = dir;
        }

        public void Reset(TweenDirection direction, bool resetDelay)
        {
            ResetAlgorithm(easeType, loopType, direction);
            _factor = 0.0f;
            if (resetDelay)
            {
                _started = false;
            }
        }

        public float ToggleFactor()
        {
            _factor = 1.0f - _factor;
            return _factor;
        }

        public void Play(TweenDirection direction)
        {
            ResetAlgorithm(easeType, loopType, direction);
            enabled = true;
        }
        public void Play(int direction)
        {
            Play((TweenDirection)direction);
        }
        public void Play(bool direction)
        {
            Play(direction ? TweenDirection.Forward : TweenDirection.Backward);
        }

        public void ResetAndPlay(TweenDirection direction, bool resetDelay)
        {
            Reset(direction, resetDelay);
            enabled = true;
        }
        public void ResetAndPlay(int direction, bool resetDelay)
        {
            Reset((TweenDirection)direction, resetDelay);
            enabled = true;
        }
        public void ResetAndPlay(bool direction, bool resetDelay)
        {
            Reset(direction ? TweenDirection.Forward : TweenDirection.Backward, resetDelay);
            enabled = true;
        }

        public static T Begin<T>(GameObject go, float duration) where T : TweenBase
        {
            T temp = go.GetComponent<T>();
            if (temp == null)
            {
                temp = go.AddComponent<T>();
            }
            temp._started = false;
            temp.duration = duration;
            temp._factor = 0.0f;
            temp._amountPerDelta = Mathf.Abs(temp.GetAmountPerDelta());
            temp.ResetAlgorithm(temp.easeType, TweenLoop.Once, TweenDirection.Forward);
            temp.OnFinished = null;
            temp.eventReceiver = null;
            temp.callWhenFinished = null;
            temp.enabled = true;
            return temp;
        }
    }
}
