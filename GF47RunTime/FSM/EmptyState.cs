namespace GF47RunTime.FSM
{
    public class EmptyState<T> : BaseState<T>
    {
        public EmptyState(int id) : base(id) { }

        public override void OnEnter() { }
        public override void OnExit() { }
    }
}
