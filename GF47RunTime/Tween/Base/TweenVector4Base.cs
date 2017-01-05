//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 15:03:42
//      Edited      :       2013/9/17 星期二 15:03:42
//************************************************************//

using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 15:03:42
    /// [TweenVector4Base] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenVector4Base : TweenBase
    {
        public List<TweenVector4> targets = new List<TweenVector4>();
        public override void SetPercent(float factor, bool isFinished)
        {
            if (targets != null)
            {
                for (int i = 0, iMax = targets.Count; i < iMax; i++)
                {
                    targets[i].Percent = Vector4.Lerp(targets[i].from, targets[i].to, factor);
                }
            }
        }

        public static TweenVector4Base Begin<T>(float duration, Vector4 from, Vector4 to, GameObject root, params GameObject[] targets) where T : TweenVector4
        {
            TweenVector4Base comp = Begin<TweenVector4Base>(root, duration);
            foreach (GameObject target in targets)
            {
                T tweener = target.GetComponent<T>();
                if (tweener == null)
                {
                    tweener = target.AddComponent<T>();
                }
                tweener.from = from;
                tweener.to = to;
                if (!comp.targets.Contains(tweener))
                {
                    comp.targets.Add(tweener);
                }
            }

            if (duration <= 0.0f)
            {
                comp.Sample(1.0f, true);
                comp.enabled = false;
            }
            return comp;
        }

    }
}
