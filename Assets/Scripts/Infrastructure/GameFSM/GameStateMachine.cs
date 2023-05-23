using Common.FSM;
using Infrastructure.Services.Logger;

namespace Infrastructure.GameFSM
{
    public class GameStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly StateMachine<IExitableState> _stateMachine;

        public GameStateMachine(ICustomLogger logger)
        {
            _logger = logger;
            _stateMachine = new StateMachine<IExitableState>();
        }

        public void EnterState<TState>() where TState : IState
        {
            _logger.Log(typeof(TState).ToString());
            _stateMachine.EnterState<TState>();
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : IStateWithArguments<TArgs>
        {
            _logger.Log(typeof(TState).ToString());
            _stateMachine.EnterState<TState, TArgs>(args);
        }

        public void AddState<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}