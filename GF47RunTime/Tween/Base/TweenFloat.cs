//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 13:42:08
//      Edited      :       2013/9/17 星期二 13:42:08
//************************************************************//

using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 13:42:08
    /// [TweenFloat] Introduction  :Nothing to introduce
    /// </summary>
    public abstract class TweenFloat : MonoBehaviour
    {
        public abstract float Percent { get; set; }
        public float from;
        public float to;
    }
}
