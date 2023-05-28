using Gameplay.Movement;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using Infrastructure.Services.Input;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private ExitableMovementState _currentState;
        
        private readonly MovementStateMachine _movementStateMachine;
        private readonly CharacterController _characterController;

        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;
        
        private readonly IUpdater _updater;
        private readonly IInputService _input;

        public PlayerMovement(MovementStateMachine movementStateMachine,
            CharacterController characterController,
            IGroundSpherecaster groundSpherecaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            IUpdater updater,
            IInputService input)
        {
            _movementStateMachine = movementStateMachine;
            _characterController = characterController;

            _groundSpherecaster = groundSpherecaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
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

            _updater.Updated -= Update;
            _input.Jumped -= OnJumpedInput;
        }

        private void Update(float deltaTime) => 
            Move(deltaTime);
        
        private void OnJumpedInput() => 
            _movementStateMachine.EnterState<JumpState>();

        private void Move(float deltaTime)
        {
            Vector3 velocity = _movementStateMachine.ActiveState.GetMoveVelocty(deltaTime);
            _characterController.Move(velocity);
        }
    }
}