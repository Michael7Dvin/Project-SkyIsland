using System.Collections.Generic;
using Common.Observable;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotation;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine.States.Base;
using UnityEngine;

namespace Gameplay.Movement.StateMachine.States.Implementations
{
    public class JumpState : MovementState
    {
        private Vector3 _currentVelocity;
        
        private float _currentJumpTime;
        private float _totalJumpTime;

        private bool _isJumped;
        
        private readonly AnimationCurve _jumpCurve;
        private readonly float _horizontalspeed;
        
        private readonly Rotator _rotator;
        private readonly GroundTypeTracker _groundTypeTracker;
        private readonly SlopeSlideMovement _slopeSlideMovement;

        private readonly IReadOnlyObservable<Vector3> _moveDirection;

        public JumpState(AnimationCurve jumpCurve,
            float horizontalspeed,
            Rotator rotator,
            GroundTypeTracker groundTypeTracker,
            SlopeSlideMovement slopeSlideMovement,
            IReadOnlyObservable<Vector3> moveDirection)
        {
            _jumpCurve = jumpCurve;
            _horizontalspeed = horizontalspeed;
            _rotator = rotator;

            _groundTypeTracker = groundTypeTracker;
            _slopeSlideMovement = slopeSlideMovement;

            _moveDirection = moveDirection;

            SetTotalTime();
        }

        public override MovementStateType Type => MovementStateType.Jump;
        public override float CurrentHorizontalSpeed => _horizontalspeed;
        public override float CurrentVerticalSpeed => _currentVelocity.y;

        protected override HashSet<GroundType> CanStartWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };

        protected override HashSet<GroundType> CanWorkWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
            GroundType.Air,
        };
        
        public override void Enter() => 
            _groundTypeTracker.CurrentGroundType.Changed += OnCurrentGroundTypeChanged;

        public override void Exit()
        {
            _isJumped = false;
            _groundTypeTracker.CurrentGroundType.Changed -= OnCurrentGroundTypeChanged;
        }

        public override bool CanStart(GroundType currentGroundType)
        {
            bool canStartBase = base.CanStart(currentGroundType);
            bool isNotSteepSlope = _slopeSlideMovement.IsSteepSlope == false;
            return canStartBase & isNotSteepSlope;
        }

        private void OnCurrentGroundTypeChanged(GroundType groundType)
        {
            if (groundType == GroundType.Ground & _isJumped == true) 
                PeformJump();

            if (groundType == GroundType.Air)
                _isJumped = true;
        }

        public override Vector3 GetMoveVelocty(float deltaTime)
        {
            Vector3 velocity = GetJumpVelocity(deltaTime) + GetHorizontalVelocity(deltaTime);

            _currentVelocity = velocity;
            
            return velocity;
        }

        public override Quaternion GetRotation(Quaternion currentRotation, float deltaTime) => 
            _rotator.GetRotationToDirection(_currentVelocity, currentRotation, deltaTime);

        private void SetTotalTime()
        {
            int jumpCurvelength = _jumpCurve.length;
            Keyframe lastKey = _jumpCurve.keys[jumpCurvelength - 1];
            _totalJumpTime = lastKey.time;
        }

        private Vector3 GetJumpVelocity(float deltaTime)
        {
            float verticalSpeed = _jumpCurve.Evaluate(_currentJumpTime);

            _currentJumpTime += deltaTime;

            if (_currentJumpTime >= _totalJumpTime)
                PeformJump();

            Vector3 velocity = Vector3.up * verticalSpeed * deltaTime;
            return velocity;
        }

        private Vector3 GetHorizontalVelocity(float deltaTime)
        {
            Vector3 velocity = new();
            
            if (_moveDirection.Value != Vector3.zero) 
                velocity = _moveDirection.Value * _horizontalspeed * deltaTime;

            return velocity;
        }
        
        private void PeformJump()
        {
            _currentJumpTime = 0f;
            NotifyMovementPerforemed();
        }
    }
}