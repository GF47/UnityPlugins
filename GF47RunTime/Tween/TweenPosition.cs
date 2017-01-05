//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 14:21:48
//      Edited      :       2013/9/17 星期二 14:21:48
//************************************************************//
namespace GF47RunTime.Tween
{

    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 14:21:48
    /// [TweenPosition] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenPosition : TweenVector3
    {
        public override Vector3 Percent
        {
            get
            {
                return target.localPosition;
            }
            set
            {
                target.localPosition = value;
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
    }
}
