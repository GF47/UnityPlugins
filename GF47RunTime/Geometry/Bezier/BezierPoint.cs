using System;
using UnityEngine;

namespace GF47RunTime.Geometry.Bezier
{
    /// <summary>
    /// 贝塞尔曲线的节点
    /// </summary>
    [Serializable]
    public class BezierPoint
    {
        public enum PointType
        {
            // Smooth,
            // Corner,
            BezierCorner,
            Bezier,
        }
        public PointType type;

        public Vector3 Point
        {
            get { return _p; }
            set
            {
                Vector3 offset = value - _p;
                _p = value;
                _handleL += offset;
                _handleR += offset;
            }
        }
        private Vector3 _p;
        public Vector3 HandleL
        {
            get { return _handleL; }
            set
            {
                switch (type)
                {
                    // case PointType.Smooth:
                    //     break;
                    // case PointType.Corner:
                    //     break;
                    case PointType.BezierCorner:
                        _handleL = value;
                        break;
                    case PointType.Bezier:
                    default:
                        float lengthL = Vector3.Distance(_handleL, _p);
                        float lengthR = Vector3.Distance(_handleR, _p);

                        _handleL = value;
                        Vector3 v = _handleL - _p;
                        if (lengthL > 0f)
                        {
                            _handleR = _p - (lengthR / lengthL) * v;
                        }
                        break;
                }
            }
        }
        private Vector3 _handleL;
        public Vector3 HandleR
        {
            get { return _handleR; }
            set
            {
                switch (type)
                {
                    // case PointType.Smooth:
                    //     break;
                    // case PointType.Corner:
                    //     break;
                    case PointType.BezierCorner:
                        _handleR = value;
                        break;
                    case PointType.Bezier:
                    default:
                        float lengthL = Vector3.Distance(_handleL, _p);
                        float lengthR = Vector3.Distance(_handleR, _p);

                        _handleR = value;
                        Vector3 v = _handleR - _p;
                        if (lengthR > 0f)
                        {
                            _handleL = _p - (lengthL / lengthR) * v;
                        }
                        break;
                }
            }
        }
        private Vector3 _handleR;

        public BezierPoint() : this(Vector3.zero, 1f, PointType.Bezier) { }
        public BezierPoint(Vector3 position) : this(position, 1f, PointType.Bezier) { }
        public BezierPoint(Vector3 position, float handleLength, PointType type)
        {
            this.type = type;
            _p = position;
            _handleL = _p + handleLength * Vector3.left;
            _handleR = _p + handleLength * Vector3.right;
        }
    }
}
