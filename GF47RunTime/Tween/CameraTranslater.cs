//************************************************************//
// Author           :GF47
// DataTime         :2013/9/10 11:37:19
// Edited           :2013/9/10 11:37:19
//************************************************************//
namespace GF47RunTime.Tween
{
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/10 11:37:19
    /// [CameraTranslater] Introduction    :nothing to introduce
    /// </summary>
    public class CameraTranslater : Tween<Vector3>
    {
        public struct CameraTargetPositions
        {
            public CameraTargetPositions(Transform target, Vector3 from, Vector3 to)
            {
                this.target = target;
                targetFrom = from;
                targetTo = to;
            }
            public CameraTargetPositions(Transform target)
            {
                this.target = target;
                targetFrom = target.localPosition;
                targetTo = target.localPosition;
            }
            public Transform target;
            public Vector3 targetFrom;
            public Vector3 targetTo;
        }

        public Transform target;
        public Vector3 targetFrom, targetTo;
        public enum TranslateMode { Linear, Interpolation }
        public TranslateMode translateMode = TranslateMode.Linear;
        
        private float _startRadius, _endRadius, _currentRadius;
        private Quaternion _startQuaternion, _endQuaternion;

        void Awake()
        {
            setValue = SetPercent;
        }

        private Vector3 SetPercent(float factor)
        {
            switch (translateMode)
            {
                case TranslateMode.Linear:
                    LinearTranslate(factor);
                    break;
                case TranslateMode.Interpolation:
                    InterpolationTranslate(factor);
                    break;
                default:
                    LinearTranslate(factor);
                    break;
            }
            return transform.localPosition;
        }

        private void LinearTranslate(float value)
        {
            transform.localPosition = Vector3.Lerp(from, to, value);
            transform.LookAt(target);
        }
        private void InterpolationTranslate(float value)
        {
            _currentRadius = Mathf.Lerp(_startRadius, _endRadius, value);
            transform.localRotation = Quaternion.Slerp(_startQuaternion, _endQuaternion, value);
            transform.localPosition = transform.localRotation * new Vector3(0.0f, 0.0f, -_currentRadius) + target.localPosition;
        }

        private void ResetValue(Vector3 f, Vector3 t, Vector3 tf, Vector3 tt)
        {
            from = f;
            to = t;
            targetFrom = tf;
            targetTo = tt;

            _startRadius = Vector3.Distance(tf, f);
            _endRadius = Vector3.Distance(tt, t);
            _startQuaternion = Quaternion.LookRotation(tf - f);
            _endQuaternion = Quaternion.LookRotation(tt - t);
        }

        public static CameraTranslater Begin(GameObject go, float duration, Vector3 from, Vector3 to, CameraTargetPositions targetPositions, TweenEase easeType, TweenLoop loopType,TranslateMode translateMode)
        {
            TweenBase tb = TweenBase.Begin<Vector3, CameraTranslater>(duration, from, to, go, go);
            tb.ResetAlgorithm(easeType, loopType, TweenDirection.Forward);
            if (tb.targets != null && tb.targets.Count > 0)
            {
                CameraTranslater ct = tb.targets[0] as CameraTranslater;
                if (ct != null)
                {
                    ct.target = targetPositions.target;
                    ct.ResetValue(from, to, targetPositions.targetFrom, targetPositions.targetTo);
                    ct.translateMode = translateMode;
                    return ct;
                }
            }
            return null;
        }
    }
}
