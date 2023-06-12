using Common.FSM;
using Cysharp.Threading.Tasks;
using Gameplay.Levels;
using Gameplay.Levels.WorldObjectsSpawning;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factory;

namespace Infrastructure.GameFSM.States
{
    public class LoadLevelState : IStateWithArguments<LevelData>
    {
        private LevelData _currentLevelData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IResourcesLoader _resourcesLoader;
        private readonly IWorldObjectsSpawnerProvider _worldObjectsSpawnerProvider;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            IResourcesLoader resourcesLoader,
            IWorldObjectsSpawnerProvider worldObjectsSpawnerProvider,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _resourcesLoader = resourcesLoader;
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

        private async void OnLevelLoaded()
        {
            _resourcesLoader.ClearCache();
            await _uiFactory.RecreateSceneUIObjects();
            await SpawnWorldObjects();
            _gameStateMachine.EnterState<GameplayState>();
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _worldObjectsSpawnerProvider.Get(_currentLevelData.Type);
            await worldObjectsSpawner.SpawnWorldObjects();
        }
    }
}