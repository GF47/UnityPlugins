//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 13:40:02
//      Edited      :       2013/9/17 星期二 13:40:02
//************************************************************//

using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 13:40:02
    /// [TweenFloatBase] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenFloatBase : TweenBase
    {
        public List<TweenFloat> targets = new List<TweenFloat>();

        public override void SetPercent(float factor, bool isFinished)
        {
            if (targets != null)
            {
                for (int i = 0, iMax = targets.Count; i < iMax; i++)
                {
                    targets[i].Percent = Mathf.Lerp(targets[i].from, targets[i].to, factor);
                }
            }
        }

        public static TweenFloatBase Begin<T>(float duration, float from, float to, GameObject root, params GameObject[] targets) where T : TweenFloat
        {
            TweenFloatBase comp = Begin<TweenFloatBase>(root, duration);
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
