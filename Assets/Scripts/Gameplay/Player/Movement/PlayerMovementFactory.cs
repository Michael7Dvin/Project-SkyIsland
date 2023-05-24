using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementFactory : IPlayerMovementFactory
    {
        private readonly PlayerMovementConfig _config;
        private readonly IUpdater _updater;
        private readonly IInputService _inputService;
        private readonly ICustomLogger _logger;

        public PlayerMovementFactory(IConfigProvider configProvider, IUpdater updater, ICustomLogger logger)
        {
            _config = configProvider.GetForPlayer().Movement;
            _updater = updater;
            _logger = logger;
        }

        public IPlayerMovement Create(CharacterController characterController, IBodyEnvironmentObserver bodyEnvironmentObserver)
        {
            StateMachine<ExitableMovementState> movementStateMachine = new();
            
            movementStateMachine.AddState(new StayState(_logger));
            movementStateMachine.AddState(new FallState(_config.FallSpeed, _updater, _logger));
            movementStateMachine.AddState(new JogState(characterController, _updater, _inputService, _logger));

            PlayerMovement movement = new(movementStateMachine, bodyEnvironmentObserver);
            
            return movement;
        }
    }
}