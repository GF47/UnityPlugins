using GF47RunTime.FSM;
using UnityEngine;

namespace UnitTestProject.Test
{
    public class FSM_Test : MonoBehaviour
    {
        private FiniStateMachine<string> _fsm;
        void Start()
        {
            _fsm = new FiniStateMachine<string>();

            IState<string> sEntry = new EmptyState<string>(FSMUtility.EntryStateID);

            IState<string> s1 = new State_0(1);
            IState<string> s2 = new State_1(2);

            sEntry.AddNextState("to s1", s1.ID);
            sEntry.AddNextState("to s2", s2.ID);

            s1.AddNextState("to s2", s2.ID);
            
            s2.AddNextState("to sEntry", sEntry.ID);

            _fsm.Add(sEntry);
            _fsm.Add(s1);
            _fsm.Add(s2);

            _fsm.StartWith(sEntry.ID);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                // Debug.Log("press 0");
                _fsm.GetInput("to s1");
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                // Debug.Log("press 1");
                _fsm.GetInput("to s2");
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                // Debug.Log("press space");
                _fsm.GetInput("to sEntry");
            }

            _fsm.Update();
        }
    }
}
