/***************************************************************
 * @File Name       : TweenBaseInspector
 * @Author          : GF47
 * @Description     : [TweenBase]类的检视面板，主要是将拖到面板上的[Monobehaviour]类填进[TweenBase.targets]中。
 * @Date            : 2017/1/6 9:25:29
 * @Edit            : none
 **************************************************************/

using System.Collections.Generic;
using GF47RunTime;
using GF47RunTime.Tween;
using UnityEngine;
using UnityEditor;

namespace GF47Editor.Editor.Inspectors
{
    [CustomEditor(typeof(TweenBase))]
    public class TweenBaseInspector : UnityEditor.Editor
    {
        private TweenBase _tweenBase;

        private bool _isFolded = true;

        void OnEnable()
        {
            _tweenBase = (TweenBase)target;
            if (_tweenBase._iPercentTargets == null)
            {
                _tweenBase._iPercentTargets = new List<MonoBehaviour>();
            }
        }

        public override void OnInspectorGUI()
        {
            _tweenBase.easeType = (TweenEase)EditorGUILayout.EnumPopup("EaseType", _tweenBase.easeType);
            _tweenBase.loopType = (TweenLoop)EditorGUILayout.EnumPopup("LoopType", _tweenBase.loopType);
            _tweenBase.useRealTime = EditorGUILayout.Toggle("UseRealTime", _tweenBase.useRealTime);
            _tweenBase.delay = EditorGUILayout.FloatField("Delay", _tweenBase.delay);
            _tweenBase.duration = EditorGUILayout.FloatField("Duration", _tweenBase.duration);
            _tweenBase.tweenGroup = EditorGUILayout.IntField("TweenGroup", _tweenBase.tweenGroup);
            _tweenBase.eventReceiver = (GameObject)EditorGUILayout.ObjectField("EventReceiver", _tweenBase.eventReceiver, typeof(GameObject), true);
            _tweenBase.callWhenFinished = EditorGUILayout.TextField("CallWhenFinished", _tweenBase.callWhenFinished);
            _tweenBase.isLateUpdate = EditorGUILayout.Toggle("isLateUpdate", _tweenBase.isLateUpdate);

            _isFolded = EditorGUILayout.Foldout(_isFolded, "Targets");
            if (_isFolded)
            {
                EditorGUILayout.BeginVertical();
                for (int i = 0; i < _tweenBase._iPercentTargets.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    Object tempObj = EditorGUILayout.ObjectField(_tweenBase._iPercentTargets[i], typeof(MonoBehaviour), true);
                    if (tempObj != null)
                    {
                        _tweenBase._iPercentTargets[i] = GetMobehaviourInheritedFromIPervent(tempObj as MonoBehaviour);
                    }
                    if (GUILayout.Button("-", EditorStyles.miniButton))
                    {
                        _tweenBase._iPercentTargets.RemoveAt(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("+"))
                {
                    _tweenBase._iPercentTargets.Add(null);
                }

                EditorGUILayout.EndVertical();
            }
        }

        private static MonoBehaviour GetMobehaviourInheritedFromIPervent(MonoBehaviour tempBehaviour)
        {
            if (tempBehaviour == null) { return null; }
            if (tempBehaviour is IPercent) { return tempBehaviour; }

            MonoBehaviour[] behaviours = tempBehaviour.GetComponents<MonoBehaviour>();
            if (behaviours != null)
            {
                for (int j = 0; j < behaviours.Length; j++)
                {
                    if (behaviours[j] is IPercent)
                    {
                        return behaviours[j];
                    }
                }
            }
            return null;
        }
    }
}
