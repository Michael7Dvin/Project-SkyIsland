﻿using Common.FSM;
using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.LevelLoading.SceneServices.WarmUppers;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners;
using Infrastructure.Progress;
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

            await SetCurrentProgress(request.Progress);

            await WarmUp();
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

        private async UniTask WarmUp()
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
            levelProgressService.Load();
        }
    }
}