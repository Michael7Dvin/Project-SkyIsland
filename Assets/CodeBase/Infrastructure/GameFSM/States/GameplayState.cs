using Common.FSM;
using Infrastructure.Services.Input;
using Infrastructure.Services.ResourcesLoading;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IInputService _inputService;

        public GameplayState(IAddressablesLoader addressablesLoader, IInputService inputService)
        {
            _addressablesLoader = addressablesLoader;
            _inputService = inputService;
        }

        public void Enter() => 
            _inputService.EnableAllInput();

        public void Exit()
        {
            _inputService.DisableAllInput();
            _addressablesLoader.ClearCache();
        }
    }
}