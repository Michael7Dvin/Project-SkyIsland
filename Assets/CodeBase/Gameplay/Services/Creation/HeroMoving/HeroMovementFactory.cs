﻿using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.Heros.Movement;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Implementations;
using Gameplay.Services.Creation.GroundSpherecasting;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.Pausing;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Gameplay.Services.Creation.HeroMoving
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
            _config = staticDataProvider.HeroConfig.Movement;
            
            _groundSpherecasterFactory = groundSpherecasterFactory;
            
            _updater = updater;
            _pauseService = pauseService;
            _input = input;
            _logger = logger;
        }

        public async UniTask WarmUp() => 
            await _groundSpherecasterFactory.WarmUp();

        public async UniTask<IHeroMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController,
            Transform camera)
        {
            IGroundSpherecaster groundSpherecaster = await CreateGroundSpherecaster(parent);
            ISlopeCalculator slopeCalculator = CreateSlopeCalculator(groundSpherecaster);
            IGroundTypeTracker groundTypeTracker = CreateGroundTypeTracker(groundSpherecaster);

            ISlopeSlideMovement slopeSlideMovement =
                new SlopeSlideMovement(_config.SlopeSlideSpeed, _config.MinSlopeAngle, slopeCalculator);

            IRotator rotator = new Rotator(_config.RotationSpeed); 
            
            IMovementStateMachine movementStateMachine = 
                CreateMovementStateMachine(camera, groundTypeTracker, slopeSlideMovement, rotator);

            HeroAnimator movementAnimator =
                new(animator, movementStateMachine.ActiveState, characterController, _updater, _logger);
            
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

        private async UniTask<IGroundSpherecaster> CreateGroundSpherecaster(Transform parent)
        {
            return await _groundSpherecasterFactory.Create(parent,
                _config.GroundSphereCastingPointOffset,
                _config.GroundSphereCastingSphereRadius,
                _config.GroundSphereCastingDistance);
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
            
            IMovementStatesProvider statesProvider = 
                new MovementStatesProvider(jogState, fallState, _logger);

            statesProvider.AddState(jogState);
            statesProvider.AddState(fallState);
            statesProvider.AddState(jumpState);

            MovementStateMachine movementStateMachine = new(statesProvider, groundTypeTracker);
            
            return movementStateMachine;
        }

        private ISlopeCalculator CreateSlopeCalculator(IGroundSpherecaster groundSpherecaster) => 
            new SlopeCalculator(groundSpherecaster);
    }
}