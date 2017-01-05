//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 13:59:11
//      Edited      :       2013/9/17 星期二 13:59:11
//************************************************************//

using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 13:59:11
    /// [TweenVector2Base] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenVector2Base : TweenBase
    {
        public List<TweenVector2> targets = new List<TweenVector2>();
        public override void SetPercent(float factor, bool isFinished)
        {
            if (targets != null)
            {
                for (int i = 0, iMax = targets.Count; i < iMax; i++)
                {
                    targets[i].Percent = Vector2.Lerp(targets[i].from, targets[i].to, factor);
                }
            }
        }

        public static TweenVector2Base Begin<T>(float duration, Vector2 from, Vector2 to, GameObject root, params GameObject[] targets) where T : TweenVector2
        {
            TweenVector2Base comp = Begin<TweenVector2Base>(root, duration);
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
