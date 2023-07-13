using Common.FSM;
using Infrastructure.Progress;
using Infrastructure.Services.ResourcesLoading;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IGameProgressService _gameProgressService;

        public GameplayState(IAddressablesLoader addressablesLoader, IGameProgressService gameProgressService)
        {
            _addressablesLoader = addressablesLoader;
            _gameProgressService = gameProgressService;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _addressablesLoader.ClearCache();
            _gameProgressService.ClearRegisteredProgressHandlers();
        }
    }
}