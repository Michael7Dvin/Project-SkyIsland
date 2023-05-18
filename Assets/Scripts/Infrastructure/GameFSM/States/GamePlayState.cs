
namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        
        public GameplayState(GameStateMachine gameStateMachine)
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