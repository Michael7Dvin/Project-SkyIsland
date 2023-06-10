using Common.FSM;
using Gameplay.Levels;
using Gameplay.Levels.WorldObjectsSpawning;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factory;

namespace Infrastructure.GameFSM.States
{
    public class LoadLevelState : IStateWithArguments<LevelData>
    {
        private LevelData _currentLevelData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWorldObjectsSpawnerProvider _worldObjectsSpawnerProvider;
        private readonly IUIFactory _uiFactory;
        
        public LoadLevelState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            IWorldObjectsSpawnerProvider worldObjectsSpawnerProvider,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _worldObjectsSpawnerProvider = worldObjectsSpawnerProvider;
            _uiFactory = uiFactory;
        }

        public void Enter(LevelData levelData)
        {
            _currentLevelData = levelData;
            _sceneLoader.Load(_currentLevelData.SceneName, OnLevelLoaded);
        }

        public void Exit()
        {
        }

        private void OnLevelLoaded()
        {
            _uiFactory.RecreateSceneUIObjects();
            SpawnWorldObjects();
        }

        private async void SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _worldObjectsSpawnerProvider.Get(_currentLevelData.Type);
            worldObjectsSpawner.SpawnWorldObjects();
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}