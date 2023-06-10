using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Implementations;
using Gameplay.Services.Pause;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Hero.Movement
{
    public class HeroMovementFactory : IHeroMovementFactory
    {
        private readonly HeroMovementConfig _config;

        private readonly IGroundSpherecasterFactory _groundSpherecasterFactory;
        
        private readonly IUpdater _updater;
        private readonly IPauseService _pauseService;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;

        public HeroMovementFactory(IStaticDataProvider staticDataProvider,
            IGroundSpherecasterFactory groundSpherecasterFactory,
            IUpdater updater,
            IPauseService pauseService,
            IInputService input,
            ICustomLogger logger)
        {
            _config = staticDataProvider.GetPlayerConfig().Movement;
            
            _groundSpherecasterFactory = groundSpherecasterFactory;
            
            _updater = updater;
            _pauseService = pauseService;
            _input = input;
            _logger = logger;
        }

        public IHeroMovement Create(Transform parent,
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

            HeroAnimator movementAnimator =
                new(animator, movementStateMachine.ActiveState, characterController, _updater, _pauseService);
            
            HeroMovement movement = new(movementStateMachine,
                characterController,
                groundSpherecaster,
                groundTypeTracker,
                slopeCalculator,
                movementAnimator,
                _updater,
                _input.Hero);
            
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
                new(_config.JogSpeed,
                    _config.JogAntiBumpSpeed,
                    slopeSlideMovement,
                    rotator,
                    camera,
                    _input.Hero.HorizontalMoveDirection);

            FallState fallState = 
                new(_config.FallVerticalSpeed,
                    _config.FallHorizontalSpeed,
                    rotator,
                    camera,
                    _input.Hero.HorizontalMoveDirection);

            JumpState jumpState = new(_config.JumpYSpeedToTimeCurve,
                _config.JumpHorizontalSpeed,
                rotator,
                camera,
                groundTypeTracker,
                slopeSlideMovement,
                _input.Hero.HorizontalMoveDirection);
            
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