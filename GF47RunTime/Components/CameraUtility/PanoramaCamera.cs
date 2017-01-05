//************************************************************//
//      Author      :       GF47
//      DataTime    :       2014/5/15 星期四 16:34:17
//      Edited      :       2014/5/15 星期四 16:34:17
//************************************************************//

using UnityEngine;

namespace GF47RunTime.Components.CameraUtility
{
    /// <summary>
    /// Author          :GF47
    /// DataTime        :2014/5/15 星期四 16:34:17
    /// [Panorama] Introduction  :Nothing to introduce
    /// </summary>
    public class PanoramaCamera : MonoBehaviour
    {
        public Transform target;
        public float distance = 100f;
        public float minDistance = 10f;
        public float maxDistance = 200f;
        public float wheelSpeed = 20f;
        [Range(0, 2)]
        public int workingButton = 0;

        public float axisXSpeed = 1f;
        public float axisYSpeed = 1f;
        public float minYLimit = 10f;
        public float maxYLimit = 80f;
        public float smoothTime = 0.5f;
        public float smoothDampMaxSpeed = 10000f;
        public float smoothDampDeltaTime = 0.02f;

        private Vector3 _lastMousePos;
        private Vector3 _saveMousePos;
        private float _smoothDistance;
        private float _velocityDistance;
        private Vector3 _smooth;
        private Vector3 _velocity;

        void OnEnable()
        {
            if (target != null)
            {
                distance = Vector3.Distance(transform.position, target.position);
                _smoothDistance = distance;
                transform.position = transform.rotation * new Vector3(0, 0, -_smoothDistance) + target.position;
            }
            _lastMousePos = Input.mousePosition;
            _saveMousePos= new Vector3(transform.eulerAngles.y, transform.eulerAngles.x, 0f);
            _saveMousePos.y = Mathf.Clamp(_saveMousePos.y, minYLimit, maxYLimit);
            _smooth = _saveMousePos;
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(workingButton))
            {
                if (Input.GetMouseButtonDown(workingButton))
                {
                    _lastMousePos = Input.mousePosition;
                }
                _saveMousePos.x += (Input.mousePosition.x - _lastMousePos.x) * axisXSpeed;
                _saveMousePos.y -= (Input.mousePosition.y - _lastMousePos.y) * axisYSpeed;
                _lastMousePos = Input.mousePosition;
                _saveMousePos.y = Mathf.Clamp(_saveMousePos.y, minYLimit, maxYLimit);
            }
            _smooth = Vector3.SmoothDamp(_smooth, _saveMousePos, ref _velocity, smoothTime, smoothDampMaxSpeed, smoothDampDeltaTime);
            transform.rotation = Quaternion.Euler(_smooth.y, _smooth.x, 0);
            if (target != null)
            {
                distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
                _smoothDistance = Mathf.SmoothDamp(_smoothDistance, distance, ref _velocityDistance, smoothTime, smoothDampMaxSpeed, smoothDampDeltaTime);
                transform.position = transform.rotation * new Vector3(0, 0, -_smoothDistance) + target.position;
            }
        }
    }
}

