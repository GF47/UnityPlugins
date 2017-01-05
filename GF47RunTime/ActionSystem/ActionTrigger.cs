namespace GF47RunTime.ActionSystem
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 行为触发器
    /// </summary>
    public class ActionTrigger : MonoBehaviour
    {
        public const int PublicGroup = MutualExclusionGroup.PublicGroup;
        internal ActionContainerList List
        {
            get
            {
                if (_list == null) { _list = new ActionContainerList(); }
                return _list;
            }
        }
        private ActionContainerList _list;
        public ActionContainer GetActionContainer(int index, bool addIfThereIsNot = true)
        {
            ActionContainer result = List[index];
            if (result == null && addIfThereIsNot)
            {
                result = new ActionContainer(this);
                _list[index] = result;
            }
            return result;
        }
        public static ActionTrigger Get(GameObject target, bool addIfThereIsNot = true)
        {
            ActionTrigger trigger = target.GetComponent<ActionTrigger>();
            if (trigger == null && addIfThereIsNot)
            {
                trigger = target.AddComponent<ActionTrigger>();
            }
            return trigger;
        }
        /// <summary>
        /// 执行节点集合列表内所有行为
        /// </summary>
        /// <param name="actionCollections">指定的节点集合列表</param>
        /// <param name="e">附加参数</param>
        private static void ExecuteAction(List<ActionContainer> actionCollections, EventArgs e)
        {
            if (actionCollections == null) return;
            for (int i = 0; i < actionCollections.Count; i++)
            {
                ActionContainer actionContainer = actionCollections[i];
                if (actionContainer != null) { actionContainer.ExecuteAction(e); }
            }
        }
        #region 触发行为
        void OnMouseDown() { ExecuteAction(List[TriggerMethod.OnMouseDown], EventArgs.Empty); }
        void OnMouseUp() { ExecuteAction(List[TriggerMethod.OnMouseUp], EventArgs.Empty); }
        void OnMouseUpAsButton() { ExecuteAction(List[TriggerMethod.OnMouseUpAsButton], EventArgs.Empty); }
        void OnMouseDrag() { ExecuteAction(List[TriggerMethod.OnMouseDrag], EventArgs.Empty); }
        void OnMouseRightDown() { ExecuteAction(List[TriggerMethod.OnMouseRightDown], EventArgs.Empty); }
        void OnMouseRightUp() { ExecuteAction(List[TriggerMethod.OnMouseRightUp], EventArgs.Empty); }
        void OnMouseRightUpAsButton() { ExecuteAction(List[TriggerMethod.OnMouseRightUpAsButton], EventArgs.Empty); }
        void OnMouseRightDrag() { ExecuteAction(List[TriggerMethod.OnMouseRightDrag], EventArgs.Empty); }
        void OnMouseEnter() { ExecuteAction(List[TriggerMethod.OnMouseEnter], EventArgs.Empty); }
        void OnMouseExit() { ExecuteAction(List[TriggerMethod.OnMouseExit], EventArgs.Empty); }
        void OnMouseOver() { ExecuteAction(List[TriggerMethod.OnMouseOver], EventArgs.Empty); }
        void OnCollisionEnter(Collision c) { ExecuteAction(List[TriggerMethod.OnCollisionEnter], new UnityEventArgs<Collision>(c)); }
        void OnCollisionExit(Collision c) { ExecuteAction(List[TriggerMethod.OnCollisionExit], new UnityEventArgs<Collision>(c)); }
        void OnCollisionStay(Collision c) { ExecuteAction(List[TriggerMethod.OnCollisionStay], new UnityEventArgs<Collision>(c)); }
        void OnTriggerEnter(Collider c) { ExecuteAction(List[TriggerMethod.OnTriggerEnter], new UnityEventArgs<Collider>(c)); }
        void OnTriggerExit(Collider c) { ExecuteAction(List[TriggerMethod.OnTriggerExit], new UnityEventArgs<Collider>(c)); }
        void OnTriggerStay(Collider c) { ExecuteAction(List[TriggerMethod.OnTriggerStay], new UnityEventArgs<Collider>(c)); }
        void Start() { ExecuteAction(List[TriggerMethod.Start], EventArgs.Empty); }
        void OnEnable() { ExecuteAction(List[TriggerMethod.OnEnable], EventArgs.Empty); }
        void OnDisable() { ExecuteAction(List[TriggerMethod.OnDisable], EventArgs.Empty); }
        void OnClick() { ExecuteAction(List[TriggerMethod.OnClick], EventArgs.Empty); }
        void OnHover(bool isOver) { ExecuteAction(List[TriggerMethod.OnHover], new UnityEventArgs<bool>(isOver)); }
        void OnPress(bool isPressed) { ExecuteAction(List[TriggerMethod.OnPress], new UnityEventArgs<bool>(isPressed)); }
        void OnSelect(bool isSelected) { ExecuteAction(List[TriggerMethod.OnSelect], new UnityEventArgs<bool>(isSelected)); }
        void OnDragStart() { ExecuteAction(List[TriggerMethod.OnDragStart], EventArgs.Empty); }
        void OnDragEnd() { ExecuteAction(List[TriggerMethod.OnDragEnd], EventArgs.Empty); }
        void OnDragOver(GameObject target) { ExecuteAction(List[TriggerMethod.OnDragOver], new UnityEventArgs<GameObject>(target)); }
        void OnDragOut(GameObject target) { ExecuteAction(List[TriggerMethod.OnDragOut], new UnityEventArgs<GameObject>(target)); }
        void OnDrag(Vector2 delta) { ExecuteAction(List[TriggerMethod.OnDrag], new UnityEventArgs<Vector2>(delta)); }
        #endregion
    }
}
