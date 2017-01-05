//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 14:15:40
//      Edited      :       2013/9/17 星期二 14:15:40
//************************************************************//

using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 14:15:40
    /// [TweenVector3Base] Introduction  :Nothing to introduce
    /// </summary>
    public class TweenVector3Base : TweenBase
    {
        public List<TweenVector3> targets = new List<TweenVector3>();

        public override void SetPercent(float factor, bool isFinished)
        {
            if (targets != null)
            {
                for (int i = 0, iMax = targets.Count; i < iMax; i++)
                {
                    targets[i].Percent = Vector3.Lerp(targets[i].from, targets[i].to, factor);
                }
            }
        }

        public static TweenVector3Base Begin<T>(float duration, Vector3 from, Vector3 to, GameObject root, params GameObject[] targets) where T : TweenVector3
        {
            TweenVector3Base comp = Begin<TweenVector3Base>(root, duration);
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
