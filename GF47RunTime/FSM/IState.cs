namespace GF47RunTime.FSM
{
    public interface IState<T>
    {
        int ID { get; }
        void GetInput(T input);
        int GetNextStateID();
        void OnEnter();
        void OnExit();
        void Reset();

        void Update();

        void AddNextState(T input, int stateID);
        void RemoveNextState(T input);
    }
}
