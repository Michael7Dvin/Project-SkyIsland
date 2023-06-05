using Common.FSM;
using Gameplay.Levels;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;

        private readonly ScenesData _scenesData;
        
        public MainMenuState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;

            _scenesData = staticDataProvider.GetScenesData();
        }

        public void Enter() => 
            _sceneLoader.Load(_scenesData.MainMenuSceneName, null);

        public void Exit()
        {
        }

        public void StartGame()
        {
            LevelData islandData = new LevelData(LevelType.Island, _scenesData.IslandSceneName);
            
            _gameStateMachine.EnterState<LoadLevelState, LevelData>(islandData);
        }
    }
}