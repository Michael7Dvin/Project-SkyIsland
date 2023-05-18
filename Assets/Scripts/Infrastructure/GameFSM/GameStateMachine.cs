using Infrastructure.Services.Logger;

namespace Infrastructure.GameFSM
{
    public class GameStateMachine
    {
        private readonly StateMachine _stateMachine;
        private readonly ICustomLogger _logger;
        
        public GameStateMachine(ICustomLogger logger)
        {
            _stateMachine = new StateMachine();
            _logger = logger;
        }

        public void EnterState<TState>() where TState : IState
        {
            _logger.Log(typeof(TState).ToString());
            _stateMachine.EnterState<TState>();
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState
        {
            _logger.Log(typeof(TState).ToString());
            _stateMachine.EnterState<TState, TArgs>(args);
        }

        public void AddState<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}