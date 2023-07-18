using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.LevelLoading.SceneServices.WarmUppers;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners;
using Infrastructure.Progress;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IStateWithArguments<LevelLoadingRequest>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneServicesProvider _sceneServicesProvider;

        public LevelLoadingState(ISceneLoader sceneLoader,
            IGameStateMachine gameStateMachine,
            ISceneServicesProvider sceneServicesProvider)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _sceneServicesProvider = sceneServicesProvider;
        }

        public async void Enter(LevelLoadingRequest request)
        {
            request.Progress.CurrentScene = request.SceneType;
            
            await LoadScene(request.SceneType);

            await SetCurrentProgress(request.Progress);

            await WarmUpServices();
            await SpawnWorldObjects();
            
            await LoadProgress();
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        private async UniTask LoadScene(SceneType sceneType)
        {
            if (_sceneLoader.CurrentScene == sceneType)
                return;
            
            await _sceneLoader.Load(sceneType);
        }

        private async UniTask SetCurrentProgress(AllProgress progress)
        {
            ILevelProgressService levelProgressService = await _sceneServicesProvider.GetProgressService();
            levelProgressService.SetCurrentProgress(progress);
        }

        private async UniTask WarmUpServices()
        {
            IWarmUpper warmUpper = await _sceneServicesProvider.GetWarmUpper();
            await warmUpper.WarmUp();
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _sceneServicesProvider.GetWorldObjectsSpawner();
            await worldObjectsSpawner.SpawnWorldObjects();
        }

        private async UniTask LoadProgress()
        {
            ILevelProgressService levelProgressService = await _sceneServicesProvider.GetProgressService();
            levelProgressService.LoadCurrentProgress();
        }
    }
}