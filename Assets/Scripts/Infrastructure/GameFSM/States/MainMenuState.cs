namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        
        public MainMenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}