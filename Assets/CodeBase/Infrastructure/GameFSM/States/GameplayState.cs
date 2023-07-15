using Common.FSM;
using Infrastructure.Services.ResourcesLoading;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IAddressablesLoader _addressablesLoader;

        public GameplayState(IAddressablesLoader addressablesLoader)
        {
            _addressablesLoader = addressablesLoader;
        }

        public void Enter()
        {
        }

        public void Exit() => 
            _addressablesLoader.ClearCache();
    }
}