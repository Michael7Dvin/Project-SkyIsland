using Gameplay.Movement;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine;
using Gameplay.Movement.StateMachine.States.Base;
using Gameplay.Movement.StateMachine.States.Implementations;
using Infrastructure.Services.Input;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private ExitableMovementState _currentState;
        
        private readonly IMovementStateMachine _movementStateMachine;
        private readonly CharacterController _characterController;

        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;
        private readonly ISlopeSlideMovement _slopeSlideMovement;

        private readonly IUpdater _updater;
        private readonly IInputService _input;

        public PlayerMovement(IMovementStateMachine movementStateMachine,
            CharacterController characterController,
            IGroundSpherecaster groundSpherecaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            ISlopeSlideMovement slopeSlideMovement,
            IUpdater updater,
            IInputService input)
        {
            _movementStateMachine = movementStateMachine;
            _characterController = characterController;

            _groundSpherecaster = groundSpherecaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
            _slopeSlideMovement = slopeSlideMovement;
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
        
        private void OnJumpedInput()
        {
            if (_slopeSlideMovement.IsSteepSlope == false)
            {
                _movementStateMachine.EnterState<JumpState>();   
            }
        }


        private void Move(float deltaTime)
        {
            Vector3 velocity = _movementStateMachine.ActiveState.GetMoveVelocty(deltaTime);
            _characterController.Move(velocity);

            Quaternion rotation =
                _movementStateMachine.ActiveState.GetRotation(_characterController.transform.rotation, deltaTime);

            _characterController.transform.rotation = rotation;
        }
    }
}