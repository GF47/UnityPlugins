namespace GF47RunTime.Updater
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Updater
    {
        public float RealTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return Time.realtimeSinceStartup;
                }
                return _realTime;
            }
        }
        private float _realTime;

        public float RealDeltaTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return _realDelta;
                }
                return 0f;
            }
        }
        private float _realDelta;

        public float RealLateTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return _realLateTime;
                }
                return 0f;
            }
        }
        private float _realLateTime;

        public float RealLateDeltaTime
        {
            get
            {
                if (Application.isPlaying)
                {
                    return _realLateDeltaTime;
                }
                return 0f;
            }
        }
        private float _realLateDeltaTime;

        public float CustomTime
        {
            get { return _customTime; }
            set { _customTime = value; }
        }
        private float _customTime;

        public Camera MainCamera
        {
            get { return _mainCamera ?? Camera.main; }
            set { _mainCamera = value; }
        }

        private Camera _mainCamera;
        private GameObject _mouseRightButtonTarget;

        public enum UpdateStyle
        {
            PerFrame,
            PerFixedFrame,
            PerAfterFrame,
            PerCustomFrame
        }

        private List<IUpdateNode> _perFrameList;
        private List<IUpdateNode> _perFixedFrameList;
        private List<IUpdateNode> _perAfterFrameList;
        private List<IUpdateNode> _perCustomFrameList;

        public Updater()
        {
            _realTime = Time.realtimeSinceStartup;

            _perFrameList = new List<IUpdateNode>();
            _perFixedFrameList = new List<IUpdateNode>();
            _perAfterFrameList = new List<IUpdateNode>();
            _perCustomFrameList = new List<IUpdateNode>();
        }

        public void Update()
        {
            float rt = Time.realtimeSinceStartup;
            _realDelta = rt - _realTime;
            _realTime = rt;

            for (int i = 0; i < _perFrameList.Count; i++)
            {
                _perFrameList[i].Update(Time.deltaTime);
            }
            for (int i = 0; i < _perCustomFrameList.Count; i++)
            {
                _perCustomFrameList[i].Update(Time.deltaTime);
            }

            #region 扩展的鼠标右键事件

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                if (MainCamera.GetCurrentHitAtMousePosition(out hit))
                {
                    _mouseRightButtonTarget = hit.collider.gameObject;
                    _mouseRightButtonTarget.SendMessage("OnMouseRightDown", SendMessageOptions.DontRequireReceiver);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (_mouseRightButtonTarget != null)
                {
                    _mouseRightButtonTarget.SendMessage("OnMouseRightUp", SendMessageOptions.DontRequireReceiver);

                    RaycastHit hit;
                    if (MainCamera.GetCurrentHitAtMousePosition(out hit))
                    {
                        if (_mouseRightButtonTarget == hit.collider.gameObject)
                        {
                            _mouseRightButtonTarget.SendMessage("OnMouseRightUpAsButton", SendMessageOptions.DontRequireReceiver);
                            _mouseRightButtonTarget = null;
                        }
                    }
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (_mouseRightButtonTarget != null)
                {
                    _mouseRightButtonTarget.SendMessage("OnMouseRightDrag", SendMessageOptions.DontRequireReceiver);
                }
            }
            #endregion
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < _perFixedFrameList.Count; i++)
            {
                _perFixedFrameList[i].Update(Time.fixedDeltaTime);
            }
        }

        public void LateUpdate()
        {
            float rt = Time.realtimeSinceStartup;
            _realLateDeltaTime = rt - _realLateTime;
            _realLateTime = rt;

            for (int i = 0; i < _perAfterFrameList.Count; i++)
            {
                _perAfterFrameList[i].Update(Time.deltaTime);
            }
        }

        public void Add(IUpdateNode node, UpdateStyle style)
        {
            switch (style)
            {
                case UpdateStyle.PerFrame:
                    if (!_perFrameList.Contains(node))
                    {
                        _perFrameList.Add(node);
                    }
                    return;
                case UpdateStyle.PerFixedFrame:
                    if (!_perFixedFrameList.Contains(node))
                    {
                        _perFixedFrameList.Add(node);
                    }
                    return;
                case UpdateStyle.PerAfterFrame:
                    if (!_perAfterFrameList.Contains(node))
                    {
                        _perAfterFrameList.Add(node);
                    }
                    return;
                case UpdateStyle.PerCustomFrame:
                    if (!_perCustomFrameList.Contains(node))
                    {
                        _perCustomFrameList.Add(node);
                    }
                    return;
            }
        }
        public bool RemoveFrom(IUpdateNode node, UpdateStyle style)
        {
            switch (style)
            {
                case UpdateStyle.PerFrame:
                    return _perFrameList.Remove(node);
                case UpdateStyle.PerFixedFrame:
                    return _perFixedFrameList.Remove(node);
                case UpdateStyle.PerAfterFrame:
                    return _perAfterFrameList.Remove(node);
                case UpdateStyle.PerCustomFrame:
                    return _perCustomFrameList.Remove(node);
            }
            return false;
        }
    }
}