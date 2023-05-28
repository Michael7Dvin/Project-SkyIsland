using System;
using Common.FSM;
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
        
        private readonly StateMachine<ExitableMovementState> _stateMachine;
        private readonly CharacterController _characterController;

        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;
        
        private readonly IUpdater _updater;
        private readonly IInputService _input;

        public PlayerMovement(StateMachine<ExitableMovementState> stateMachine,
            CharacterController characterController,
            IGroundSpherecaster groundSpherecaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            IUpdater updater,
            IInputService input)
        {
            _stateMachine = stateMachine;
            _characterController = characterController;

            _groundSpherecaster = groundSpherecaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
            _updater = updater;
            _input = input;

            _stateMachine.EnterState<FallState>();
            
            _groundTracker.CurrentGroundType.Changed += OnGroundTypeChanged;

            _stateMachine.ActiveState.Changed += OnStateChanged;
            _updater.Updated += Update;
            _input.Jumped += OnJumpedInput;
        }

        private bool IsSlideDownSlope
        {
            get
            {
                bool isGrounded = _groundTracker.CurrentGroundType.Value == GroundType.Ground;
                bool isSlopeGreaterThanLimit = _slopeCalculator.SlopeAngle >= _characterController.slopeLimit;

                return isGrounded && isSlopeGreaterThanLimit;
            }
        }
        
        public void Dispose()
        {
            _groundSpherecaster.Dispose();
            _groundTracker.Dispose();
            _slopeCalculator.Dispose();
            
            _groundTracker.CurrentGroundType.Changed -= OnGroundTypeChanged;
            _stateMachine.Dispose();

            _stateMachine.ActiveState.Changed -= OnStateChanged;
            _updater.Updated -= Update;
            _input.Jumped -= OnJumpedInput;
        }

        private void Update(float deltaTime) => 
            Move(deltaTime);

        private void OnStateChanged(ExitableMovementState state)
        {
            if (_currentState != null) 
                _currentState.Completed -= OnStateCompleted;
            
            _currentState = state;
            _currentState.Completed += OnStateCompleted;
        }

        private void OnStateCompleted() => 
            SetDefaultStateByGroundType(_groundTracker.CurrentGroundType.Value);

        private void OnGroundTypeChanged(GroundType groundType)
        {
            if (_stateMachine.ActiveState.Value.IsWorkableWithBodyEnvironmentType(groundType) == false)
                SetDefaultStateByGroundType(groundType);
        }

        private void SetDefaultStateByGroundType(GroundType groundType)
        {
            switch (groundType)
            {
                case GroundType.Ground:
                    _stateMachine.EnterState<JogState>();
                    break;
                case GroundType.Air:
                    _stateMachine.EnterState<FallState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
            }    
        }

        private void OnJumpedInput()
        {
            if (_currentState.GetType() != typeof(JumpState))
            {
                _stateMachine.EnterState<JumpState>();
            }
        }

        private void Move(float deltaTime)
        {
            Vector3 velocity = _stateMachine.ActiveState.Value.MoveVelocity;
            _characterController.Move(velocity);
            
            if (IsSlideDownSlope)
            {
                SlideDownSlope(deltaTime);
            }
        }
        
        private void SlideDownSlope(float deltaTime)
        {
            Vector3 velocity = _slopeCalculator.SlopeDirection * -5f * deltaTime;
            _characterController.Move(velocity);
        }
    }
}