//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 15:48:07
//      Edited      :       2013/9/17 星期二 15:48:07
//************************************************************//
namespace GF47RunTime.Tween
{

    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 15:48:07
    /// [TweenTransform] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenTransform : TweenBase
    {
        public Transform from;
        public Transform to;

        private Transform _transform;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;

        public override void SetPercent(float factor, bool isFinished)
        {
            if (to != null)
            {
                if (_transform == null)
                {
                    _transform = transform;
                    _position = _transform.position;
                    _rotation = _transform.rotation;
                    _scale = _transform.localScale;
                }

                if (from != null)
                {
                    _transform.position = Vector3.Lerp(from.position, to.position, factor);
                    _transform.localScale = Vector3.Lerp(from.localScale, to.localScale, factor);
                    _transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, factor);
                }
                else
                {
                    _transform.position = Vector3.Lerp(_position, to.position, factor);
                    _transform.localScale = Vector3.Lerp(_scale, to.localScale, factor);
                    _transform.rotation = Quaternion.Slerp(_rotation, to.rotation, factor);
                }
            }
        }

        public static TweenTransform Begin(GameObject go, float duration, Transform to)
        {
            return Begin(go, duration, null, to);
        }
        public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
        {
            TweenTransform comp = Begin<TweenTransform>(go, duration);
            comp.from = from;
            comp.to = to;

            if (duration <= 0.0f)
            {
                comp.Sample(1.0f, true);
                comp.enabled = false;
            }
            return comp;
        }
    }
}
