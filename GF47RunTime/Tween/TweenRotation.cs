//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 14:41:26
//      Edited      :       2013/9/17 星期二 14:41:26
//************************************************************//
namespace GF47RunTime.Tween
{

    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 14:41:26
    /// [TweenRotation] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenRotation : TweenVector3
    {
        public override Vector3 Percent
        {
            get
            {
                return target.localEulerAngles;
            }
            set
            {
                target.localEulerAngles = value;
            }
        }

        public Transform target;

        void Awake()
        {
            if (target == null)
            {
                target = transform;
            }
        }

        [ContextMenu("复制Transform到From")]
        private void TransformToFrom()
        {
            from = transform.localEulerAngles;
        }

        [ContextMenu("复制Transform到To")]
        private void TransformToTo()
        {
            to = transform.localEulerAngles;
        }

        [ContextMenu("复制From到Transform")]
        private void FromToTransform()
        {
            transform.localEulerAngles = from;
        }

        [ContextMenu("复制To到Transform")]
        private void ToToTransform()
        {
            transform.localEulerAngles = to;
        }
    }
}
