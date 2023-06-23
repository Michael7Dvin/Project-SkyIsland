using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Base;
using Gameplay.Movement.StateMachine.States.Implementations;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Gameplay.Heros.Movement
{
    public class HeroMovement : IHeroMovement
    {
        private ExitableMovementState _currentState;
        
        private readonly IMovementStateMachine _movementStateMachine;
        private readonly CharacterController _characterController;

        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;
        private readonly HeroAnimator _animator;

        private readonly IUpdater _updater;
        private readonly IHeroInput _input;

        public HeroMovement(IMovementStateMachine movementStateMachine,
            CharacterController characterController,
            IGroundSpherecaster groundSpherecaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            HeroAnimator animator,
            IUpdater updater,
            IHeroInput input)
        {
            _movementStateMachine = movementStateMachine;
            _characterController = characterController;

            _groundSpherecaster = groundSpherecaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
            _animator = animator;
            _updater = updater;
            _input = input;

            _updater.Updated += Update;
            _input.Jumped += OnJumpedInput;
        }
        
        public void Dispose()
        {
            _movementStateMachine.Dispose();
            
            _groundSpherecaster.Dispose();
            _groundTracker.Dispose();
            _slopeCalculator.Dispose();
            _animator.Dispose();
            
            _updater.Updated -= Update;
            _input.Jumped -= OnJumpedInput;
        }

        private void Update(float deltaTime) => 
            Move(deltaTime);

        private void OnJumpedInput() => 
            _movementStateMachine.EnterState<JumpState>();

        private void Move(float deltaTime)
        {
            ExitableMovementState activeState = _movementStateMachine.ActiveState.Value;
            
            Vector3 velocity = activeState.GetMoveVelocty(deltaTime);
            _characterController.Move(velocity);

            Quaternion rotation =
                activeState.GetRotation(_characterController.transform.rotation, deltaTime);

            _characterController.transform.rotation = rotation;
        }
    }
}