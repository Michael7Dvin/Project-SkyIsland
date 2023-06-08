using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Implementations;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
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

        public PlayerMovementFactory(IStaticDataProvider staticDataProvider,
            IGroundSpherecasterFactory groundSpherecasterFactory,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger)
        {
            _config = staticDataProvider.GetPlayerConfig().Movement;
            
            _groundSpherecasterFactory = groundSpherecasterFactory;
            
            _updater = updater;
            _input = input;
            _logger = logger;
        }

        public IPlayerMovement Create(Transform parent,
            Animator animator,
            CharacterController characterController,
            Transform camera)
        {
            IGroundSpherecaster groundSpherecaster = CreateGroundSpherecaster(parent);
            ISlopeCalculator slopeCalculator = CreateSlopeCalculator(groundSpherecaster);
            IGroundTypeTracker groundTypeTracker = 
                CreateGroundTypeTracker(groundSpherecaster);

            ISlopeSlideMovement slopeSlideMovement =
                new SlopeSlideMovement(_config.SlopeSlideSpeed, _config.MinSlopeAngle, slopeCalculator);

            IRotator rotator = new Rotator(_config.RotationSpeed); 
            
            IMovementStateMachine movementStateMachine = 
                CreateMovementStateMachine(camera, groundTypeTracker, slopeSlideMovement, rotator);

            PlayerAnimator movementAnimator = new(animator, movementStateMachine.ActiveState, characterController, _updater);
            
            PlayerMovement movement = new(movementStateMachine,
                characterController,
                groundSpherecaster,
                groundTypeTracker,
                slopeCalculator,
                movementAnimator,
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

        private MovementStateMachine CreateMovementStateMachine(Transform camera,
            IGroundTypeTracker groundTypeTracker, ISlopeSlideMovement slopeSlideMovement, IRotator rotator)
        {
            JogState jogState = 
                new(_config.JogSpeed, _config.JogAntiBumpSpeed, slopeSlideMovement, rotator, camera, _input);

            FallState fallState = 
                new(_config.FallVerticalSpeed, _config.FallHorizontalSpeed, rotator, camera, _input);

            JumpState jumpState = new(_config.JumpYSpeedToTimeCurve,
                _config.JumpHorizontalSpeed,
                rotator,
                camera,
                groundTypeTracker,
                slopeSlideMovement,
                _input);
            
            IMovementStateProvider stateProvider = 
                new MovementStateProvider(jogState, fallState, _logger);

            stateProvider.AddState(jogState);
            stateProvider.AddState(fallState);
            stateProvider.AddState(jumpState);

            MovementStateMachine movementStateMachine = new(stateProvider, groundTypeTracker);
            
            return movementStateMachine;
        }

        private ISlopeCalculator CreateSlopeCalculator(IGroundSpherecaster groundSpherecaster) => 
            new SlopeCalculator(groundSpherecaster);
    }
}