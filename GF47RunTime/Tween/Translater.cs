//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/18 星期三 17:39:04
//      Edited      :       2013/9/18 星期三 17:39:04
//************************************************************//
namespace GF47RunTime.Tween
{
    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/18 星期三 17:39:04
    /// [Translater] 目标物体移动
    /// </summary>
    public class Translater : TweenBase
    {
        public Vector3 from,to;

        public override void SetPercent(float factor, bool isFinished)
        {
            transform.localPosition = Vector3.Lerp(from, to, factor);
        }

        public static Translater Begin(GameObject go, float duration, Vector3 from, Vector3 to, TweenEase easeType, TweenLoop loopType)
        {
            Translater comp = Begin<Translater>(go, duration);
            comp.tweenGroup = PublicGroup;
            comp.from = from;
            comp.to = to;
            comp.ResetAlgorithm(easeType, loopType, TweenDirection.Forward);
            comp.loopType = loopType;
            
            if (duration <= 0.0f)
            {
                comp.Sample(1.0f, true);
                comp.enabled = false;
            }
            return comp;
        }
    }

}
