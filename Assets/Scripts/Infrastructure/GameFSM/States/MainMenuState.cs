using Infrastructure.SceneLoading;
using Infrastructure.Services;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UtilityDataProvider _utilityDataProvider;
        
        public MainMenuState(GameStateMachine gameStateMachine, UtilityDataProvider utilityDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _utilityDataProvider = utilityDataProvider;
        }

        public void Enter()
        {
            StartGame();
        }

        public void Exit()
        {
        }

        private void StartGame()
        {
            SceneLoadRequest request = new(_utilityDataProvider.ScenesInfo.IslandSceneName, OnLevelLoaded);
            _gameStateMachine.EnterState<LoadSceneState, SceneLoadRequest>(request);
        }
        
        public void OnLevelLoaded()
        {
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}