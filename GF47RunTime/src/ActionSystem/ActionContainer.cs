namespace GF47RunTime.ActionSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 行为节点集合
    /// </summary>
    public class ActionContainer : IEnumerable, IDisposable
    {
        public ActionContainer(ActionTrigger t)
        {
            _disposed = false;
            _actionTrigger = t;
            _actionNodes = new List<ActionNode>(); 
            ActionCount = 1;
        }
        private ActionTrigger _actionTrigger;
        /// <summary>
        /// 行为节点列表，不为空
        /// </summary>
        internal List<ActionNode> ActionNodes { get { return _actionNodes; } }
        private List<ActionNode> _actionNodes;
        /// <summary>
        /// 集合中所有行为的数量，大于等于1
        /// </summary>
        public int ActionCount
        {
            get { return _actionNodes.Count; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                while (_actionNodes.Count < value) { _actionNodes.Add(new ActionNode()); }
                while (_actionNodes.Count > value) { _actionNodes.RemoveAt(_actionNodes.Count - 1); }
            }
        }
        /// <summary>
        /// 指向当前行为节点的索引
        /// </summary>
        public int CurrentIndex
        {
            get { return _currentIndex; }
            private set
            {
                if (_actionNodes.Count == 0)
                {
                    _currentIndex = 0;
                    return;
                }
                _currentIndex = value % _actionNodes.Count;
                if (_currentIndex < 0) { _currentIndex += _actionNodes.Count; }
            }
        }
        private int _currentIndex;
        /// <summary>
        /// 行为节点集合所在的互斥组，0组内的行为不互斥
        /// </summary>
        public int Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    MutualExclusionGroup.RemoveFromTheGroup(this);
                    _group = value;
                    MutualExclusionGroup.AddToTheGroup(this);
                }
            }
        }
        private int _group;
        public TriggerMethod TriggerMethod
        {
            get { return _triggerMethod; }
            set
            {
                _triggerMethod = value;
                if (_actionTrigger == null) { return; }
                _actionTrigger.List.AddToListSortedByTrigger(this);
            }
        }
        private TriggerMethod _triggerMethod = TriggerMethod.None;

        /// <summary>
        /// 统一设置此行为节点集合实例
        /// </summary>
        /// <param name="triggerMethod">可以触发本结合的方法</param>
        /// <param name="group">行为节点集合所在的互斥组，0组内的行为不互斥</param>
        /// <param name="actionCount">集合中所有行为的数量，大于等于1</param>
        /// <param name="currentIndex">指向当前行为节点的索引</param>
        /// <returns>本行为节点集合实例</returns>
        public ActionContainer Set(TriggerMethod triggerMethod, int group = MutualExclusionGroup.PublicGroup, int actionCount = 1, int currentIndex = 0)
        {
            TriggerMethod = triggerMethod;
            Group = group;
            ActionCount = actionCount;
            CurrentIndex = currentIndex;
            return this;
        }

        /// <summary>
        /// 在指定节点上注册事件
        /// </summary>
        /// <param name="action">具体事件</param>
        /// <param name="isRegister">true代表注册，false代表取消注册</param>
        /// <param name="index">指定节点的索引</param>
        public void Register(Action<ActionTrigger, EventArgs> action, bool isRegister = true, int index = 0)
        {
            if (action == null) { return; }
            if (index < 0 || index >= ActionCount) { return; }
            _actionNodes[index].Register(action, isRegister);
        }
        /// <summary>
        /// 执行当前节点的行为，并指向下一个节点
        /// </summary>
        /// <param name="e">附加参数</param>
        public void ExecuteAction(EventArgs e)
        {
            if (_actionNodes == null) { return; }
            MutualExclusionGroup.ResetTheGroup(_group, this);
            ActionNode node = _actionNodes[CurrentIndex++];
            if (node != null) { node.Action(_actionTrigger, e); }
        }
        /// <summary>
        /// 执行指定节点前一个节点的行为，并指向指定节点
        /// </summary>
        /// <param name="index">指定节点的索引值</param>
        /// <param name="e">附加参数</param>
        public void ExecuteActionTo(int index, EventArgs e)
        {
            if (_actionNodes == null) { return; }
            if (CurrentIndex != index)
            {
                MutualExclusionGroup.ResetTheGroup(_group, this);
                CurrentIndex = index - 1;
                ActionNode node = _actionNodes[CurrentIndex++];
                if (node != null)
                {
                    node.Action(_actionTrigger, e);
                }
            }
        }
        /// <summary>
        /// 重置本行为节点集合，回到0状态
        /// </summary>
        public void ResetActions()
        {
            if (_actionNodes == null) { return; }
            if (CurrentIndex != 0)
            {
                CurrentIndex = ActionCount - 1;
                ActionNode node = _actionNodes[CurrentIndex++];
                if (node != null)
                {
                    node.Action(_actionTrigger, null);
                }
            }
        }
        /// <summary>
        /// 循环访问本节点集合的枚举数
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return _actionNodes.GetEnumerator();
        }

        #region Dispose

        private bool _disposed;
        /// <summary>
        /// 清理节点集合，将所有节点上的事件全部取消
        /// </summary>
        public void ClearActions()
        {
            for (int i = 0; i < _actionNodes.Count; i++)
            {
                _actionNodes[i].Dispose();
            }
            _actionNodes.Clear();
            if (_actionTrigger != null)
            {
                _actionTrigger.List.RemoveFromListSortedByTrigger(this);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ClearActions();
                    _actionNodes = null;
                }
                _disposed = true;
            }
        }
        ~ActionContainer()
        {
            Dispose(false);
        }

        #endregion
    }
}
