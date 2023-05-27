using System;
using Common.FSM;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private readonly StateMachine<ExitableMovementState> _stateMachine;
        private readonly CharacterController _characterController;

        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;
        private readonly IUpdater _updater;

        public PlayerMovement(StateMachine<ExitableMovementState> stateMachine,
            CharacterController characterController,
            IGroundSpherecaster groundSpherecaster,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator,
            IUpdater updater)
        {
            _stateMachine = stateMachine;
            _characterController = characterController;

            _groundSpherecaster = groundSpherecaster;
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;
            _updater = updater;

            _stateMachine.EnterState<FallState>();
            _groundTracker.CurrentGroundType.Changed += OnGroundTypeChanged;

            _updater.Updated += Update;
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
            _updater.Updated -= Update;
            
            _groundTracker.CurrentGroundType.Changed -= OnGroundTypeChanged;
            _stateMachine.Dispose();
            
            _groundSpherecaster.Dispose();
            _groundTracker.Dispose();
            _slopeCalculator.Dispose();
        }

        private void Update(float deltaTime) => 
            Move(deltaTime);

        private void OnGroundTypeChanged(GroundType groundType)
        {
            if (_stateMachine.ActiveState.Value.IsWorkableWithBodyEnvironmentType(groundType) == false)
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