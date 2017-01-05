//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/1/9 星期四 10:14:11
//      Edited      :       2014/1/9 星期四 10:14:11
//************************************************************//
namespace GF47RunTime.Tween
{
    using Base;
    using UnityEngine;

    /// <summary>
    /// Author          :GF47
    /// DataTime        :2014/1/9 星期四 10:14:11
    /// [TweenCameraMatrix] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenCameraMatrix : TweenBase
    {
        public Camera target;

        [Range(0, 3)]
        public int row;
        [Range(0, 3)]
        public int column;

        public float from, to;

        public override void SetPercent(float factor, bool isFinished)
        {
            if (target != null)
            {
                Matrix4x4 m = target.projectionMatrix;
                m[Mathf.Clamp(row, 0, 3), Mathf.Clamp(column, 0, 3)] = Mathf.Lerp(from, to, factor);
                target.projectionMatrix = m;
            }
        }

        public static TweenCameraMatrix Begin(float duration, float from, float to, GameObject root)
        {
            TweenCameraMatrix comp = Begin<TweenCameraMatrix>(root, duration);
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
