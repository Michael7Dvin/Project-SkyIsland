using System.Threading.Tasks;
using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Implementations;
using Gameplay.Player.Movement;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;

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
            DebugPlayerMovement(_updater, _logger);
        }

        public void Exit()
        {
        }

        private async void DebugPlayerMovement(IUpdater updater, ICustomLogger logger)
        {
            PlayerMovement playerMovement = new PlayerMovement(updater, logger);
            
            _logger.Log("Start of Debug");
            
            _logger.Log("Player On the Ground");
            playerMovement.EnterStateForDebug<DebugStayState>();
            playerMovement.BodyEnvironmentObserverPublicForDebug.SetBodyEnvironmentTypeForDebug(BodyEnvironmentType.Grounded);
            await Task.Delay(2000);
            
            _logger.Log("Player In Air");
            playerMovement.BodyEnvironmentObserverPublicForDebug.SetBodyEnvironmentTypeForDebug(BodyEnvironmentType.InAir);
            await Task.Delay(20);
            
            _logger.Log("Player On the Ground");
            playerMovement.BodyEnvironmentObserverPublicForDebug.SetBodyEnvironmentTypeForDebug(BodyEnvironmentType.Grounded);
            
            playerMovement.Dispose();
            
            _logger.Log("End of Debug");
        }
    }
}