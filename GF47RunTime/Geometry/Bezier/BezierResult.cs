using System;
using UnityEngine;

namespace GF47RunTime.Geometry.Bezier
{
    [Serializable]
    public class BezierResult
    {
        public readonly Vector3 position;
        public readonly Vector3 velocity;

        public Vector3 Direction
        {
            get { return velocity.normalized; }
        }

        public BezierResult(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }
}
