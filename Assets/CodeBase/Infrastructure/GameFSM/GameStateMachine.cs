using Common.FSM;
using Infrastructure.Services.Logging;

namespace Infrastructure.GameFSM
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly ICustomLogger _logger;
        private readonly StateMachine<IExitableState> _stateMachine = new();

        public GameStateMachine(ICustomLogger logger)
        {
            _logger = logger;
        }
        
        public void EnterState<TState>() where TState : IState
        {
            _logger.Log($"Entered: {typeof(TState).Name}");
            _stateMachine.EnterState<TState>();
        }

        public void EnterState<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>
        {
            _logger.Log($"Entered: {typeof(TState).Name}");
            _stateMachine.EnterState<TState, TArgument>(argument);
        }

        public void AddState<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}