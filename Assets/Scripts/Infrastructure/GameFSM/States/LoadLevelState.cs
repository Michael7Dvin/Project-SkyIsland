namespace Infrastructure.GameFSM.States
{
    public class LoadLevelState : IStateWithArguments<SceneLoadRequest>
    {
        private readonly GameStateMachine _gameStateMachine;
        
        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(SceneLoadRequest request)
        {
            _gameStateMachine.EnterState<GamePlayState>();
        }

        public void Exit()
        {

        }
    }
}