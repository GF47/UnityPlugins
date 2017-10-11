﻿namespace GF47RunTime.FSM
{
    public delegate void ExecuteHandler();
    public interface IState<T>
    {
        int ID { get; }
        void GetInput(T input);
        int GetNextStateID();
        void OnEnter(int lastID);
        void OnExit(int nextID);
        void Reset();

        void Update();

        void AddNextState(T input, int stateID);
        void RemoveNextState(T input);
    }
}
