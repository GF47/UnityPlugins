//************************************************************//
// Author           :GF47
// DataTime         :2013/9/10 11:37:19
// Edited           :2013/9/10 11:37:19
//************************************************************//
namespace GF47RunTime.Tween
{
    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/10 11:37:19
    /// [CameraTranslater] Introduction    :nothing to introduce
    /// </summary>
    public class CameraTranslater : TweenBase
    {
        public struct CameraTargetPositions
        {
            public CameraTargetPositions(Transform target, Vector3 from, Vector3 to)
            {
                theTarget = target;
                targetFromPosition = from;
                targetToPosition = to;
            }
            public CameraTargetPositions(Transform target)
            {
                theTarget = target;
                targetFromPosition = target.localPosition;
                targetToPosition = target.localPosition;
            }
            public Transform theTarget;
            public Vector3 targetFromPosition;
            public Vector3 targetToPosition;
        }
        public Transform target;
        public Vector3 from, to, targetFrom, targetTo;
        public enum TranslateMode { Linear, Interpolation }
        public TranslateMode translateMode = TranslateMode.Linear;
        
        private float _startRadius, _endRadius, _currentRadius;
        private Quaternion _startQuaternion, _endQuaternion;

        public override void SetPercent(float factor, bool isFinished)
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
            CameraTranslater comp = Begin<CameraTranslater>(go, duration);
            comp.tweenGroup = PublicGroup;
            comp.target = targetPositions.theTarget;
            comp.ResetValue(from, to, targetPositions.targetFromPosition, targetPositions.targetToPosition);
            comp.ResetAlgorithm(easeType, loopType, TweenDirection.Forward);
            comp.loopType = loopType;
            comp.translateMode = translateMode;

            if (duration <= 0.0f)
            {
                comp.Sample(1.0f, true);
                comp.enabled = false;
            }
            return comp;
        }

        //public static CameraTranslater Begin(GameObject go, float duration, Vector3 from, Vector3 to, Vector3 targetFrom, Vector3 targetTo, TweenEase easeType, TweenLoop loopType, TranslateMode translateMode)
        //{
        //    CameraTranslater comp = Begin<CameraTranslater>(go, duration);
        //    comp.tweenGroup = PublicGroup;
        //    comp.ResetValue(from, to, targetFrom, targetTo);
        //    comp.ResetAlgorithm(easeType, loopType, TweenDirection.Forward);
        //    comp.translateMode = translateMode;
        //    if (duration <= 0f)
        //    {
        //        comp.Sample(1f, true);
        //        comp.enabled = false;
        //    }
        //    return comp;
        //}
    }
}
