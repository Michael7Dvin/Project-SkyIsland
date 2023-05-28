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
        private readonly PlayerMovementConfig _config;

        private readonly IGroundSpherecasterFactory _groundSpherecasterFactory;
        
        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;
        
        public PlayerMovementFactory(IConfigProvider configProvider,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger)
        {
            _config = configProvider.GetForPlayer().MovementConfig;
            
            _updater = updater;
            _input = input;
            _logger = logger;
            
            _groundSpherecasterFactory = new GroundSpherecasterFactory(_updater);
        }

        public IPlayerMovement Create(Transform parent, CharacterController characterController, Transform camera)
        {
            IGroundSpherecaster groundSpherecaster = CreateGroundSpherecaster(parent);
            IGroundTypeTracker groundTypeTracker = CreateGroundTypeTracker(groundSpherecaster);
            ISlopeCalculator slopeCalculator = CreateSlopeCalculator(groundSpherecaster);
            
            StateMachine<ExitableMovementState> movementStateMachine = CreateMovementStateMachine(camera);

            PlayerMovement movement = new(movementStateMachine,
                characterController,
                groundSpherecaster,
                groundTypeTracker,
                slopeCalculator,
                _updater,
                _input);
            
            return movement;
        }

        private IGroundSpherecaster CreateGroundSpherecaster(Transform parent)
        {
            return _groundSpherecasterFactory.Create(parent, _config.GroundSphereCastingPointPrefab,
                _config.GroundSphereCastingSphereRadius, _config.GroundSphereCastingDistance);
        }
        
        private IGroundTypeTracker CreateGroundTypeTracker(IGroundSpherecaster groundSpherecaster) => 
            new GroundTypeTracker(groundSpherecaster);

        private StateMachine<ExitableMovementState> CreateMovementStateMachine(Transform camera)
        {
            StateMachine<ExitableMovementState> movementStateMachine = new();

            JogState jogState = new(_config.JogSpeed, _config.JogAntiBumpSpeed, camera, _updater, _input, _logger);
            
            FallState fallState = new(_config.FallVerticalSpeed,
                _config.FallHorizontalSpeed,
                camera,
                _updater,
                _input,
                _logger);
            
            JumpState jumpState = new(_config.JumpYSpeedToTimeCurve,
                _config.JumpHorizontalSpeed,
                camera,
                _updater,
                _input,
                _logger);
            
            movementStateMachine.AddState(jogState);
            movementStateMachine.AddState(fallState);
            movementStateMachine.AddState(jumpState);
            
            return movementStateMachine;
        }

        private ISlopeCalculator CreateSlopeCalculator(IGroundSpherecaster groundSpherecaster) => 
            new SlopeCalculator(groundSpherecaster);
    }
}