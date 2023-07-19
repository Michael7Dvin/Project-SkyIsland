using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading;
using Infrastructure.Progress;
using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.SceneServicesProviding;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class LevelLoadingState : IStateWithArgument<LevelLoadRequest>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly IAddressablesLoader _addressablesLoader;

        public LevelLoadingState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ISceneServicesProvider sceneServicesProvider,
            IAddressablesLoader addressablesLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _sceneServicesProvider = sceneServicesProvider;
            _addressablesLoader = addressablesLoader;
        }

        public async void Enter(LevelLoadRequest request)
        {
            _addressablesLoader.ClearCache();
            
            await _sceneLoader.Load(request.SceneID);

            SetCurrentProgress(request.Progress);

            await WarmUp();
            await SpawnWorldObjects();
            
            LoadProgress();

            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        private void SetCurrentProgress(AllProgress progress)
        {
            ILevelProgressService levelProgressService = _sceneServicesProvider.ProgressService;
            levelProgressService.SetCurrentProgress(progress);
        }

        private async UniTask WarmUp()
        {
            IWarmUpper warmUpper = _sceneServicesProvider.WarmUpper;
            await warmUpper.WarmUp();
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = _sceneServicesProvider.WorldObjectsSpawner;
            await worldObjectsSpawner.SpawnWorldObjects();
        }

        private void LoadProgress()
        {
            ILevelProgressService levelProgressService = _sceneServicesProvider.ProgressService;
            levelProgressService.Load();
            levelProgressService.Save();
        }
    }
}