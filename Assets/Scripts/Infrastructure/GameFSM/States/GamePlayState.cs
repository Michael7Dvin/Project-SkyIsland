using System.Threading.Tasks;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public GameplayState(IUpdater updater, ICustomLogger logger)
        {
            _updater = updater;
            _logger = logger;
        }

        public void Enter()
        {
            DebugUpdater();
        }

        public void Exit()
        {
        }
        
        private async void DebugUpdater()
        {
            _logger.Log("Creating listener1...");
            await Task.Delay(4000);
            UpdatesListenerForDebug listener1 = new(_updater, _logger);
            await Task.Delay(3000);
            listener1.Dispose();
            _logger.Log("listener1 disposed");
            
            _logger.Log("Creating listener2...");
            await Task.Delay(4000);
            UpdatesListenerForDebug listener2 = new(_updater, _logger);
            await Task.Delay(3000);
            listener2.Dispose();
            _logger.Log("listener2 disposed");
        }
    }
}