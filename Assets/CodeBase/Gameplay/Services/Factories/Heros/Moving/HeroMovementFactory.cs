using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.Heros.Movement;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Implementations;
using Gameplay.Services.Factories.GroundSpherecasting;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Factories.Heros.Moving
{
    public class HeroMovementFactory : IHeroMovementFactory
    {
        private readonly HeroMovementConfig _config;
        private readonly IGroundSpherecasterFactory _groundSpherecasterFactory;
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;
        private readonly ICustomLogger _logger;

        public HeroMovementFactory(IStaticDataProvider staticDataProvider,
            IGroundSpherecasterFactory groundSpherecasterFactory,
            IInstantiator instantiator,
            IInputService inputService,
            ICustomLogger logger)
        {
            _config = staticDataProvider.HeroConfig.Movement;
            _groundSpherecasterFactory = groundSpherecasterFactory;
            _instantiator = instantiator;
            _inputService = inputService;
            _logger = logger;
        }

        public async UniTask WarmUp() => 
            await _groundSpherecasterFactory.WarmUp();

        public async UniTask<IMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController)
        {
            IGroundSphereCaster groundSphereCaster = await CreateGroundSpherecaster(parent);
            ISlopeCalculator slopeCalculator = CreateSlopeCalculator(groundSphereCaster);
            IGroundTypeTracker groundTypeTracker = CreateGroundTypeTracker(groundSphereCaster);

            ISlopeSlideMovement slopeSlideMovement =
                new SlopeSlideMovement(_config.SlopeSlideSpeed, _config.MinSlopeAngle, slopeCalculator);

            IRotator rotator = new Rotator(_config.RotationSpeed); 
            
            IMovementStateMachine movementStateMachine = 
                CreateMovementStateMachine(groundTypeTracker, slopeSlideMovement, rotator);
            
            HeroAnimator movementAnimator = _instantiator.Instantiate<HeroAnimator>();
            movementAnimator.Construct(animator, movementStateMachine.ActiveState, characterController);

            HeroMovement heroMovement = _instantiator.Instantiate<HeroMovement>();
            heroMovement.Construct(movementStateMachine,
                characterController,
                groundSphereCaster,
                groundTypeTracker,
                slopeCalculator,
                movementAnimator);
            
            return heroMovement;
        }

        private async UniTask<IGroundSphereCaster> CreateGroundSpherecaster(Transform parent)
        {
            return await _groundSpherecasterFactory.Create(parent,
                _config.GroundSphereCastingPointOffset,
                _config.GroundSphereCastingSphereRadius,
                _config.GroundSphereCastingDistance);
        }
        
        private IGroundTypeTracker CreateGroundTypeTracker(IGroundSphereCaster groundSphereCaster) => 
            new GroundTypeTracker(groundSphereCaster);

        private MovementStateMachine CreateMovementStateMachine(IGroundTypeTracker groundTypeTracker,
            ISlopeSlideMovement slopeSlideMovement,
            IRotator rotator)
        {
            JogState jogState = 
                new(_config.JogSpeed,
                    _config.JogAntiBumpSpeed,
                    slopeSlideMovement,
                    rotator,
                    _inputService.Hero.HorizontalMoveDirection);

            FallState fallState = 
                new(_config.FallVerticalSpeed,
                    _config.FallHorizontalSpeed,
                    rotator,
                    _inputService.Hero.HorizontalMoveDirection);

            JumpState jumpState = new(_config.JumpYSpeedToTimeCurve,
                _config.JumpHorizontalSpeed,
                rotator,
                groundTypeTracker,
                slopeSlideMovement,
                _inputService.Hero.HorizontalMoveDirection);
            
            IMovementStatesProvider statesProvider = 
                new MovementStatesProvider(jogState, fallState, _logger);

            statesProvider.AddState(jogState);
            statesProvider.AddState(fallState);
            statesProvider.AddState(jumpState);

            MovementStateMachine movementStateMachine = new(statesProvider, groundTypeTracker);
            
            return movementStateMachine;
        }

        private ISlopeCalculator CreateSlopeCalculator(IGroundSphereCaster groundSphereCaster) => 
            new SlopeCalculator(groundSphereCaster);
    }
}