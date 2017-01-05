//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 14:44:27
//      Edited      :       2013/9/17 星期二 14:44:27
//************************************************************//
namespace GF47RunTime.Tween
{

    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 14:44:27
    /// [TweenScale] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenScale : TweenVector3
    {
        public override Vector3 Percent
        {
            get
            {
                return target.localScale;
            }
            set
            {
                target.localScale = value;
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
