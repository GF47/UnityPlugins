//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 14:15:23
//      Edited      :       2013/9/17 星期二 14:15:23
//************************************************************//

using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 14:15:23
    /// [TweenVector3] Introduction  :Nothing to introduce
    /// </summary>
    public abstract class TweenVector3 : MonoBehaviour
    {
        public abstract Vector3 Percent { get; set; }
        public Vector3 from;
        public Vector3 to;
    }
}
