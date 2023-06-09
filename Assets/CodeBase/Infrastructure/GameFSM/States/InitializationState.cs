using Common.FSM;
using Infrastructure.Services.Input.Service;

namespace Infrastructure.GameFSM.States
{
    public class InitializationState : IState
    {
        private readonly IInputService _inputService;
        private readonly IGameStateMachine _gameStateMachine;

        public InitializationState(IInputService inputService, IGameStateMachine gameStateMachine)
        {
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _inputService.Init();
            _gameStateMachine.EnterState<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}