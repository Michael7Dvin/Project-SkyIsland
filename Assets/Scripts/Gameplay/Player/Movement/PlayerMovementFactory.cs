using Common.FSM;
using Gameplay.GroundTypeObserving;
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

        public PlayerMovementFactory(IConfigProvider configProvider,
            IUpdater updater,
            IInputService inputService,
            ICustomLogger logger)
        {
            _config = configProvider.GetForPlayer().Movement;
            _updater = updater;
            _inputService = inputService;
            _logger = logger;
        }

        public IPlayerMovement Create(CharacterController characterController,
            IGroundTypeObserver groundTypeObserver, Transform camera)
        {
            StateMachine<ExitableMovementState> movementStateMachine = new();
            
            movementStateMachine.AddState(new StayState(_logger));
            movementStateMachine.AddState(new FallState(_config.FallSpeed, characterController, _updater, _logger));

            JogState jogState = new(_config.JogSpeed, characterController, _updater, _inputService, _logger, camera);
            movementStateMachine.AddState(jogState);

            PlayerMovement movement = new(movementStateMachine, groundTypeObserver);
            
            return movement;
        }
    }
}