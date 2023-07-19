using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners;
using Infrastructure.Progress;

namespace Infrastructure.GameFSM.States
{
    public class LevelRestartState : IStateWithArgument<AllProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneServicesProvider _sceneServicesProvider;

        public LevelRestartState(IGameStateMachine gameStateMachine, ISceneServicesProvider sceneServicesProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneServicesProvider = sceneServicesProvider;
        }

        public async void Enter(AllProgress progress)
        {
            await SetCurrentProgress(progress);

            await SpawnWorldObjects();
            
            await LoadProgress();
            
            _gameStateMachine.EnterState<GameplayState>();
        }

        public void Exit()
        {
        }

        private async UniTask SetCurrentProgress(AllProgress progress)
        {
            ILevelProgressService levelProgressService = await _sceneServicesProvider.GetProgressService();
            levelProgressService.SetCurrentProgress(progress);
        }

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = await _sceneServicesProvider.GetWorldObjectsSpawner();
            await worldObjectsSpawner.SpawnWorldObjects();
        }

        private async UniTask LoadProgress()
        {
            ILevelProgressService levelProgressService = await _sceneServicesProvider.GetProgressService();
            levelProgressService.Load();
        }
    }
}