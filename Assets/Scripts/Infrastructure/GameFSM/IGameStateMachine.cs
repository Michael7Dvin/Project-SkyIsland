using Common.FSM;

namespace Infrastructure.GameFSM
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IState;
        void EnterState<TState, TArgs>(TArgs args) where TState : IStateWithArguments<TArgs>;
        void AddState<TState>(TState state) where TState : IExitableState;
    }
}