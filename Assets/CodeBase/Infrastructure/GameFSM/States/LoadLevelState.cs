using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;
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
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            IResourcesLoader resourcesLoader,
            ILevelServicesProvider levelServicesProvider,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _resourcesLoader = resourcesLoader;
            _levelServicesProvider = levelServicesProvider;
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
            CleanUp();
            
            await WarmUpServices();
            await _uiFactory.RecreateSceneUIObjects();
            await SpawnWorldObjects();
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        private void CleanUp() => 
            _resourcesLoader.ClearCache();

        private async UniTask WarmUpServices()
        {
            IWarmUpper warmUpper = await _levelServicesProvider.GetWarmUpper(_currentLevelData.Type);
            await warmUpper.WarmUp();
        }
        
        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = 
                await _levelServicesProvider.GetWorldObjectsSpawner(_currentLevelData.Type);
            await worldObjectsSpawner.SpawnWorldObjects();
        }
    }
}