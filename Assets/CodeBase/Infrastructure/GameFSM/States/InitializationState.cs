using Common.FSM;
using DG.Tweening;
using Infrastructure.Services.Input;
using Infrastructure.Services.Updating;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;

        public InitializationState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IUpdater updater)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _updater = updater;
        }

        public async void Enter()
        {
            await Addressables.InitializeAsync().Task;
            DOTween.Init();

            _inputService.Init();

            _updater.StartUpdating();
            
            _gameStateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}