﻿//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/2/24 22:43:09
//      Edited      :       2014/2/24 22:43:09
//************************************************************//

namespace GF47RunTime.Tween.Base
{
    public enum TweenEase
    {
        Linear,
        QuadEaseIn, QuadEaseOut, QuadEaseInOut,
        CubicEaseIn, CubicEaseOut, CubicEaseInOut,
        QuartEaseIn, QuartEaseOut, QuartEaseInOut,
    }
    public enum TweenLoop
    {
        Once = 0,
        Loop,
        PingPong
    }
    public enum TweenDirection
    {
        Forward = 1,
        Backward = -1,
        Toggle = 0
    }
}
