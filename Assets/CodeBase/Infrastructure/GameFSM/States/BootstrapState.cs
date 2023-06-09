using Common.FSM;

namespace Infrastructure.GameFSM.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter() => 
            _gameStateMachine.EnterState<InitializationState>();

        public void Exit()
        {
        }
    }
}