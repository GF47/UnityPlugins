//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 13:56:56
//      Edited      :       2013/9/17 星期二 13:56:56
//************************************************************//

using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 13:56:56
    /// [TweenVector2] Introduction  :Nothing to introduce
    /// </summary>
    public abstract class TweenVector2 : MonoBehaviour
    {
        public abstract Vector2 Percent { get; set; }
        public Vector2 from;
        public Vector2 to;
    }
}
