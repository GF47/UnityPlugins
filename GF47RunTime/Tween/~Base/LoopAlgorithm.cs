//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/3/2 3:11:30
//      Edited      :       2014/3/2 3:11:30
//************************************************************//

namespace GF47RunTime.Tween
{
    /// <summary>
    /// 循环算法
    /// </summary>
    internal class LoopAlgorithm
    {
        public LoopAlgorithm(TweenLoop loopType)
        {
            _type = (int)loopType;
            _direction = 1;
        }
        public Factor Result(float percent)
        {
            switch (_type)
            {
                case 0: // Once
                    _factor = percent;
                    return new Factor(_factor.Clamp(0f, 1f), _factor >= 1.0f);
                case 1: // Loop
                    _factor = percent;
                    return new Factor(_factor % 1.0f, false);
                case 2: // PingPong
                    if (percent > 1.0f)
                    {
                        _direction *= -1;
                        percent %= 1.0f;
                    }
                    _factor = _direction > 0f ? percent : 1f - percent;
                    return new Factor(_factor, false);
                default:
                    return new Factor(_factor, _factor >= 1.0f);
            }
        }
        public TweenLoop LoopType
        {
            get { return (TweenLoop)_type; }
            set { _type = (int)value; }
        }

        private int _type;
        private int _direction;
        private float _factor;
    }
}