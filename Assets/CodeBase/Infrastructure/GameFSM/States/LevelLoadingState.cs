using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.Progress;
using Infrastructure.Progress.LevelProgressLoading;
using Infrastructure.Services.LevelLoading;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.LevelServicesProviding;
using Infrastructure.Services.LevelLoading.WarmUpping;
using Infrastructure.Services.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.UI;

namespace Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IStateWithArguments<LevelData>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IUIFactory _uiFactory;
        private readonly IGameProgressService _gameProgressService;

        public LevelLoadingState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            ILevelServicesProvider levelServicesProvider,
            IUIFactory uiFactory,
            IGameProgressService gameProgressService)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _levelServicesProvider = levelServicesProvider;
            _uiFactory = uiFactory;
            _gameProgressService = gameProgressService;
        }

        public async void Enter(LevelData levelData)
        {
            await _sceneLoader.Load(levelData.Scene);
            
            await InitializeProgress(levelData);
            
            await WarmUpServices(levelData.Type);
            await _uiFactory.RecreateSceneUIObjects();
            await SpawnWorldObjects(levelData.Type);

            _gameProgressService.LoadCurrentProgress();
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        private async UniTask InitializeProgress(LevelData levelData)
        {
            IProgressLoader progressLoader 
                = await _levelServicesProvider.GetProgressInitializer(levelData.Type);

            progressLoader.InitializeProgressHandlers(levelData);
            progressLoader.RegisterProgressHandlers();
        }

        private async UniTask WarmUpServices(LevelType levelType)
        {
            IWarmUpper warmUpper = await _levelServicesProvider.GetWarmUpper(levelType);
            await warmUpper.WarmUp();
        }

        private async UniTask SpawnWorldObjects(LevelType levelType)
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _levelServicesProvider.GetWorldObjectsSpawner(levelType);
            await worldObjectsSpawner.SpawnWorldObjects();
        }
    }
}