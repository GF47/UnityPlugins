//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/2/13 星期四 16:37:34
//      Edited      :       2014/2/13 星期四 16:37:34
//************************************************************//
namespace GF47RunTime.Tween
{
    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2014/2/13 星期四 16:37:34
    /// [TweenCameraFOV] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenCameraFOV : TweenFloat
    {
        public override float Percent
        {
            get { return target.fieldOfView; }
            set { target.fieldOfView = value; }
        }

        public Camera target;
        void Awake()
        {
            if (target == null)
            {
                target = GetComponent<Camera>();
            }
        }
    }
}
