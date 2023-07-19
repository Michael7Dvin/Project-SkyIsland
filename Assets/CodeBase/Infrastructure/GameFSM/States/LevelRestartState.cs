using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.Progress;
using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.SceneServicesProviding;
using Infrastructure.SceneServices.WorldObjectsSpawners;

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
            SetCurrentProgress(progress);

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

        private async UniTask SpawnWorldObjects()
        {
            IWorldObjectsSpawner worldObjectsSpawner = _sceneServicesProvider.WorldObjectsSpawner;
            await worldObjectsSpawner.SpawnWorldObjects();
        }

        private void LoadProgress()
        {
            ILevelProgressService levelProgressService = _sceneServicesProvider.ProgressService;
            levelProgressService.Load();
        }
    }
}