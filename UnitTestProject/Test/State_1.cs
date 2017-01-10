using GF47RunTime.FSM;

namespace UnitTestProject.Test
{
    public class State_1 : BaseState<string>
    {
        public override bool CanExitSafely
        {
            get { return _canExitSafely; }
            protected set { _canExitSafely = value; }
        }

        private bool _canExitSafely;

        public State_1(int id) : base(id) { } 
        public override void OnEnter()
        {
            UnityEngine.Debug.Log("enter state 1");
        }

        public override void OnExit()
        {
            UnityEngine.Debug.Log("exit state 1");
        }

        public override void Update()
        {
            UnityEngine.Debug.Log("stay at state 1");
        }
    }
}
