using Common.FSM;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
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
        private readonly IUpdater _updater;
        private readonly IInputService _inputService;
        private readonly ICustomLogger _logger;

        private readonly PlayerMovementConfig _config;

        private readonly IGroundSpherecasterFactory _groundSpherecasterFactory;

        public PlayerMovementFactory(IConfigProvider configProvider,
            IUpdater updater,
            IInputService inputService,
            ICustomLogger logger)
        {
            _config = configProvider.GetForPlayer().Movement;
            
            _updater = updater;
            _inputService = inputService;
            _logger = logger;
            
            _groundSpherecasterFactory = new GroundSpherecasterFactory(_updater);
        }

        public IPlayerMovement Create(Transform parent, CharacterController characterController, Transform camera)
        {
            IGroundSpherecaster groundSpherecaster = CreateGroundSpherecaster(parent);
            IGroundTypeTracker groundTypeTracker = CreateGroundTypeTracker(groundSpherecaster);
            ISlopeCalculator slopeCalculator = CreateSlopeCalculator(groundSpherecaster);
            
            StateMachine<ExitableMovementState> movementStateMachine = 
                CreateMovementStateMachine(characterController, camera, slopeCalculator);

            PlayerMovement movement = new(movementStateMachine, groundTypeTracker, slopeCalculator);
            
            return movement;
        }

        private IGroundSpherecaster CreateGroundSpherecaster(Transform parent)
        {
            return _groundSpherecasterFactory.Create(parent, _config.GroundSphereCastingPointPrefab,
                _config.GroundSphereCastingSphereRadius, _config.GroundSphereCastingDistance);
        }
        
        private IGroundTypeTracker CreateGroundTypeTracker(IGroundSpherecaster groundSpherecaster) => 
            new GroundTypeTracker(groundSpherecaster);

        private ISlopeCalculator CreateSlopeCalculator(IGroundSpherecaster groundSpherecaster) => 
            new SlopeCalculator(groundSpherecaster);

        private StateMachine<ExitableMovementState> CreateMovementStateMachine(CharacterController characterController,
            Transform camera,
            ISlopeCalculator slopeCalculator)
        {
            StateMachine<ExitableMovementState> movementStateMachine = new();

            movementStateMachine.AddState(new StayState(_logger));
            movementStateMachine.AddState(new FallState(_config.FallSpeed, characterController, _updater, _logger));

            JogState jogState = 
                new(_config.JogSpeed, characterController, slopeCalculator, _updater, _inputService, _logger, camera);
            movementStateMachine.AddState(jogState);
            
            return movementStateMachine;
        }
    }
}