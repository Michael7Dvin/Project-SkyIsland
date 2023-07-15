using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Base;
using Gameplay.Movement.StateMachine.States.Implementations;
using Infrastructure.Services.Input;
using Infrastructure.Services.Input.Handlers.Hero;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Gameplay.Heros.Movement
{
    public class HeroMovement : IMovement
    {
        private ExitableMovementState _currentState;
        
        private IMovementStateMachine _movementStateMachine;
        private CharacterController _characterController;
        private IGroundSphereCaster _groundSphereCaster;
        private IGroundTypeTracker _groundTracker;
        private ISlopeCalculator _slopeCalculator;
        private HeroAnimator _animator;

        private readonly IUpdater _updater;
        private readonly IHeroInput _input;

        public HeroMovement(IUpdater updater, IInputService inputService)
        {
            _updater = updater;
            _input = inputService.Hero;
        }

        public void Construct(IMovementStateMachine movementStateMachine,
            CharacterController characterController,
            IGroundSphereCaster groundSphereCaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            HeroAnimator animator)
        {
            _movementStateMachine = movementStateMachine;
            _characterController = characterController;

            _groundSphereCaster = groundSphereCaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
            _animator = animator;
            
            _updater.Updated += Update;
            _input.Jumped += OnJumpedInput;
        }
        
        public void Dispose()
        {
            _movementStateMachine.Dispose();
            
            _groundSphereCaster.Dispose();
            _groundTracker.Dispose();
            _slopeCalculator.Dispose();
            _animator.Dispose();
            
            _updater.Updated -= Update;
            _input.Jumped -= OnJumpedInput;
        }

        public void Teleport(Vector3 position)
        {
            _characterController.enabled = false;
            _characterController.transform.position = position;
            _characterController.enabled = true;
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