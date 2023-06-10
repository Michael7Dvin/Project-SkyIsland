using Common.FSM;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Updater;
using UI.Services.Factory;
using UI.Services.WindowsOperating;

namespace Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        private readonly IInputService _inputService;
        private readonly IUpdater _updater;
        private readonly IUIFactory _uiFactory;
        
        public InitializationState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IUpdater updater,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _updater = updater;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _inputService.Init();
            _uiFactory.Init();
            
            _updater.StartUpdating();

            _gameStateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}