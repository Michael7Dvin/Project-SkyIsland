using Common.FSM;
using Gameplay.Levels;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class LoadLevelState : IStateWithArguments<LevelData>
    {
        private LevelData _currentLevelData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWorldObjectsSpawnerProvider _worldObjectsSpawnerProvider;
        
        public LoadLevelState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IWorldObjectsSpawnerProvider worldObjectsSpawnerProvider)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _worldObjectsSpawnerProvider = worldObjectsSpawnerProvider;
        }

        public void Enter(LevelData levelData)
        {
            _currentLevelData = levelData;
            _sceneLoader.Load(_currentLevelData.SceneName, OnLevelLoaded);
        }

        public void Exit()
        {
        }

        private void OnLevelLoaded() => 
            SpawnWorldObjects();

        private async void SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _worldObjectsSpawnerProvider.Get(_currentLevelData.Type);
            worldObjectsSpawner.SpawnWorldObjects();
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}