using System;

namespace GF47RunTime.Updater
{
    public class FloatBuffer : AbstractValueBuffer<float>
    {
        public FloatBuffer(float initialValue, Action<float> callback, float duration) : base(initialValue, callback, duration) { }

        public FloatBuffer(float initialValue, Action<float> callback) : base(initialValue, callback) { }

        protected override float Addition(float a, float b) { return a + b; }

        protected override float Subtraction(float a, float b) { return a - b; }

        protected override float Multiplication(float multiplier, float originValue) { return multiplier * originValue; }
        protected override bool Division(float originValue, float divisor, out float result)
        {
            if (Math.Abs(divisor) < 1e-6f)
            {
                result = originValue;
                return false;
            }

            result = originValue / divisor;
            return true;
        }


        protected override bool IsLengthGreaterThanTMin(float value)
        {
            return Math.Abs(value) > 1e-6f;
        }

        protected override float Projection(float value, float start, float end)
        {
            if (Math.Abs(end - start) < 1e-6f) { return 1f; }
            return (value - start) / (end - start);
        }
    }
}
