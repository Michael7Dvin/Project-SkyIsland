using System;
using Common.FSM;
using DG.Tweening;
using Infrastructure.Services.Input;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.Updating;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;

        public InitializationState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IInputService inputService,
            IUpdater updater)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _inputService = inputService;
            _updater = updater;
        }

        public async void Enter()
        {
            await Addressables.InitializeAsync().Task;
            DOTween.Init();

            _sceneLoader.Initailize();
            _inputService.Initialize();
            _inputService.DisableAllInput();
            
            _updater.StartUpdating();
            
            _gameStateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}