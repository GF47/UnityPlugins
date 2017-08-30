/***************************************************************
 * @File Name       : Vector3Buffer
 * @Author          : GF47
 * @Description     : TODO what's the use of the [Vector3Buffer]
 * @Date            : 2017/8/16/星期三 11:29:37
 * @Edit            : none
 **************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GF47RunTime.Updater
{
    public class Vector3Buffer : AbstractValueBuffer<Vector3>
    {
        public Vector3Buffer(Vector3 initialValue, Action<Vector3> callback, float duration) : base(initialValue, callback, duration) { } 
        public Vector3Buffer(Vector3 initialValue, Action<Vector3> callback) : base(initialValue, callback) { } 

        protected override Vector3 Addition(Vector3 a, Vector3 b)
        {
            return a + b;
        }

        protected override Vector3 Subtraction(Vector3 a, Vector3 b)
        {
            return a - b;
        }

        protected override Vector3 Multiplication(float multiplier, Vector3 originValue)
        {
            return multiplier * originValue;
        }

        protected override bool Division(Vector3 originValue, float divisor, out Vector3 result)
        {
            if (Math.Abs(divisor) < 1e-6f)
            {
                result = originValue;
                return false;
            }

            result = originValue / divisor;
            return true;
        }

        protected override bool IsLengthGreaterThanTMin(Vector3 value)
        {
            return value.magnitude > 1e-6f;
        }

        protected override float Projection(Vector3 value, Vector3 start, Vector3 end)
        {
            Vector3 divisor = end - start;
            if (divisor.magnitude < 1e-6f) { return 1f; }
            Vector3 p = Vector3.Project(value - start, divisor);
            if (divisor.x > 1e-6f) { return p.x / divisor.x; }
            if (divisor.y > 1e-6f) { return p.y / divisor.y; }
            if (divisor.z > 1e-6f) { return p.z / divisor.z; }
            return 1f;
        }
    }
}
