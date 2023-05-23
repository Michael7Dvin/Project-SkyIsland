using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementFactory : IPlayerMovementFactory
    {
        private readonly PlayerMovementConfig _config;
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public PlayerMovementFactory(IConfigProvider configProvider, IUpdater updater, ICustomLogger logger)
        {
            _config = configProvider.GetForPlayer().Movement;
            _updater = updater;
            _logger = logger;
        }

        public PlayerMovement Create(CharacterController characterController, IBodyEnvironmentObserver bodyEnvironmentObserver)
        {
            StateMachine<IExitableMovementState> movementStateMachine = new();
            
            movementStateMachine.AddState(new DebugStayState(_logger));
            movementStateMachine.AddState(new DebugFallState(_config.FallSpeed, _updater, _logger));

            PlayerMovement movement = new(movementStateMachine, bodyEnvironmentObserver);
            
            return movement;
        }
    }
}