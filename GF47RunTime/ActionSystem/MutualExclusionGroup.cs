namespace GF47RunTime.ActionSystem
{
    using System.Collections.Generic;

    internal class MutualExclusionGroup
    {
        /// <summary>
        /// 不互斥的组，值为0
        /// </summary>
        public const int PublicGroup = 0;

        /// <summary>
        /// 互斥的行为节点集合组唯一实例
        /// </summary>
        private static SortedList<int, List<ActionContainer>> _actionCollectionGroup = new SortedList<int, List<ActionContainer>>();
        public static bool AddToTheGroup(ActionContainer actionContainer)
        {
            if (actionContainer == null) { return false; }
            int group = actionContainer.Group;
            if (!_actionCollectionGroup.ContainsKey(group)) { _actionCollectionGroup.Add(group, new List<ActionContainer>()); }
            _actionCollectionGroup[group].Add(actionContainer);
            return true;
        }
        public static bool RemoveFromTheGroup(ActionContainer actionContainer)
        {
            if (actionContainer == null) { return false; }
            int group = actionContainer.Group;
            if (!_actionCollectionGroup.ContainsKey(group)) { return false; }
            return _actionCollectionGroup[group].Remove(actionContainer);
        }
        public static void ResetTheGroup(int group, ActionContainer ignore)
        {
            if (group == PublicGroup) { return; }
            if (!_actionCollectionGroup.ContainsKey(group)) { return; }
            List<ActionContainer> list = _actionCollectionGroup[group];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == ignore) { continue; }
                list[i].ResetActions();
            }
        }
    }
}