using Common.FSM;
using Infrastructure.Services.Input;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IInputService _inputService;

        public GameplayState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Enter() => 
            _inputService.EnableAllInput();

        public void Exit() => 
            _inputService.DisableAllInput();
    }
}