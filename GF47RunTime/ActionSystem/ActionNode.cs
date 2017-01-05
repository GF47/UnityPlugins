namespace GF47RunTime.ActionSystem
{
    using System;

    /// <summary>
    /// 单个行为节点
    /// </summary>
    internal class ActionNode : IDisposable
    {
        /// <summary>
        /// 执行节点绑定的事件
        /// </summary>
        private event Action<ActionTrigger, EventArgs> ExecuteAction;

        /// <summary>
        /// 执行节点绑定的事件
        /// </summary>
        /// <param name="sender">行为的发起者</param>
        /// <param name="e">附加参数</param>
        public void Action(ActionTrigger sender, EventArgs e)
        {
            if (ExecuteAction == null) return;
            ExecuteAction(sender, e);
        }

        /// <summary>
        /// 在节点上注册事件
        /// </summary>
        /// <param name="action">具体事件</param>
        /// <param name="isRegister">true代表注册，false代表取消注册</param>
        public void Register(Action<ActionTrigger, EventArgs> action, bool isRegister)
        {
            if (action == null) return;
            if (isRegister) ExecuteAction += action;
            else ExecuteAction -= action;
        }
        /// <summary>
        /// 清理节点，将节点上所有的事件全部取消
        /// </summary>
        public void Dispose()
        {
            ExecuteAction = null;
        }
    }
}
