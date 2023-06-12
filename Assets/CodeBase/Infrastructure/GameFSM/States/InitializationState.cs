using Common.FSM;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Updater;
using UI.Services.Factory;
using UI.Windows.Factory;

namespace Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;
        private readonly IWindowFactory _windowFactory;
        private readonly IUIFactory _uiFactory;
        
        public InitializationState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IUpdater updater,
            IWindowFactory windowFactory,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _updater = updater;
            _windowFactory = windowFactory;
            _uiFactory = uiFactory;
        }

        public async void Enter()
        {
            _inputService.Init();
            _uiFactory.Init();

            await _uiFactory.WarmUp();
            await _windowFactory.WarmUp();
            
            _updater.StartUpdating();
            
            _gameStateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}