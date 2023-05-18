namespace Infrastructure.GameFSM
{
    public class GameStateMachine
    {
        private readonly StateMachine _stateMachine;

        public GameStateMachine()
        {
            _stateMachine = new StateMachine();
        }

        public void EnterState<TState>() where TState : IState =>
            _stateMachine.EnterState<TState>();

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState =>
            _stateMachine.EnterState<TState, TArgs>(args);

        public void AddState<TState>(TState state) where TState : IExitableState =>
            _stateMachine.AddState(state);
    }
}