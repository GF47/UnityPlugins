using System.Collections.Generic;

namespace GF47RunTime.FSM
{
    public abstract class BaseState<T> : IState<T>
    {
        private int _id;
        private SortedList<T, int> _nextStates;
        private int _nextStateID;

        public int ID { get { return _id; } }

        protected BaseState(int id)
        {
            if (!FSMUtility.IsLogicalStateID(id))
            {
                throw new System.ArgumentException(string.Format("请将 [ID] 设置为一个非 [{0}] 的数值", FSMUtility.NullStateID), "id");
            }
            _id = id; 
            _nextStates = new SortedList<T, int>();
            _nextStateID = _id;
        }

        public void GetInput(T input)
        {
            if (_nextStates.ContainsKey(input))
            {
                _nextStateID = _nextStates[input];
            }
        }

        public int GetNextStateID()
        {
            return _nextStateID;
        }

        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Reset()
        {
            _nextStateID = _id;
        }

        public virtual void Update() { } 

        public void AddNextState(T input, int stateID)
        {
            _nextStates.Add(input, stateID);
        }

        public void RemoveNextState(T input)
        {
            _nextStates.Remove(input);
        }
    }
}
