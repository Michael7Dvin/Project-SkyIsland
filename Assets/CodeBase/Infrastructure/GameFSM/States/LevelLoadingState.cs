using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.LevelServicesProviding;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Progress;
using Infrastructure.Progress.Services;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.UI;

namespace Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IStateWithArguments<LevelLoadingRequest>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IUIFactory _uiFactory;

        public LevelLoadingState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            ILevelServicesProvider levelServicesProvider,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _levelServicesProvider = levelServicesProvider;
            _uiFactory = uiFactory;
        }

        public async void Enter(LevelLoadingRequest request)
        {
            await _sceneLoader.Load(request.LevelData.Scene);

            await SetCurrentProgress(request.Progress);

            await WarmUpServices();
            await _uiFactory.RecreateSceneUIObjects();
            await SpawnWorldObjects();
            
            await LoadProgress();
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        private async UniTask SetCurrentProgress(AllProgress progress)
        {
            ILevelProgressService levelProgressService = await _levelServicesProvider.GetProgressService();
            levelProgressService.SetCurrentProgress(progress);
        }

        private async UniTask WarmUpServices()
        {
            IWarmUpper warmUpper = await _levelServicesProvider.GetWarmUpper();
            await warmUpper.WarmUp();
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _levelServicesProvider.GetWorldObjectsSpawner();
            await worldObjectsSpawner.SpawnWorldObjects();
        }

        private async UniTask LoadProgress()
        {
            ILevelProgressService levelProgressService = await _levelServicesProvider.GetProgressService();
            levelProgressService.LoadCurrentProgress();
        }
    }
}