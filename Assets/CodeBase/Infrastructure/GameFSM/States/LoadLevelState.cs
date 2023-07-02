using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.LevelServicesProviding;
using Infrastructure.Services.LevelLoading.WarmUpping;
using Infrastructure.Services.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.UI;

namespace Infrastructure.GameFSM.States
{
    public class LoadLevelState : IStateWithArguments<LevelData>
    {
        private LevelData _currentLevelData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            IAddressablesLoader addressablesLoader,
            ILevelServicesProvider levelServicesProvider,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _addressablesLoader = addressablesLoader;
            _levelServicesProvider = levelServicesProvider;
            _uiFactory = uiFactory;
        }

        public async void Enter(LevelData levelData)
        {
            _currentLevelData = levelData;
            await _sceneLoader.Load(_currentLevelData.Scene);
            
            CleanUp();
            
            await WarmUpServices();
            await _uiFactory.RecreateSceneUIObjects();
            await SpawnWorldObjects();
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }
        
        private void CleanUp() => 
            _addressablesLoader.ClearCache();

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