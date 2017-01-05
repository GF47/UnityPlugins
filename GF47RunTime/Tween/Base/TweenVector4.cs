//************************************************************//
//      Author      :       GF47
//      DataTime    :       2013/9/17 星期二 15:03:29
//      Edited      :       2013/9/17 星期二 15:03:29
//************************************************************//

using UnityEngine;

namespace GF47RunTime.Tween.Base
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2013/9/17 星期二 15:03:29
    /// [TweenVector4] Introduction  :Nothing to introduce
    /// </summary>
    public abstract class TweenVector4 : MonoBehaviour
    {
        public abstract Vector4 Percent { get; set; }
        public Vector4 from;
        public Vector4 to;
    }
}
