﻿using GF47RunTime.FSM;

namespace UnitTestProject.Test
{
    public class State_0 : BaseState<string>
    {
        public State_0(int id) : base(id) { }

        public override void OnEnter(int lastID)
        {
            UnityEngine.Debug.Log("enter state 0");
        }

        public override void OnExit(int nextID)
        {
            UnityEngine.Debug.Log("exit state 0");
        }

        public override void Update()
        {
            UnityEngine.Debug.Log("stay at state 0");
        }
    }
}
