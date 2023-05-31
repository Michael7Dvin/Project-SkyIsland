using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine.States.Base;
using Infrastructure.Services.Input;
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
        
        private readonly IRotator _rotator;
        private readonly Transform _camera;
        private readonly IGroundTypeTracker _groundTypeTracker;
        private readonly ISlopeSlideMovement _slopeSlideMovement;

        private readonly IInputService _input;

        public JumpState(AnimationCurve jumpCurve,
            float horizontalspeed,
            IRotator rotator,
            Transform camera,
            IGroundTypeTracker groundTypeTracker,
            ISlopeSlideMovement slopeSlideMovement,
            IInputService input)
        {
            _jumpCurve = jumpCurve;
            _horizontalspeed = horizontalspeed;
            _rotator = rotator;

            _camera = camera;
            _groundTypeTracker = groundTypeTracker;
            _slopeSlideMovement = slopeSlideMovement;

            _input = input;

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

        public override void Dispose() => 
            _groundTypeTracker.CurrentGroundType.Changed -= OnCurrentGroundTypeChanged;

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
            
            if (_input.HorizontalDirection.Value != Vector3.zero)
            {
                Vector3 cameraAlignedDirection = AlignDirectionToCameraView(_input.HorizontalDirection.Value);
                velocity = cameraAlignedDirection * _horizontalspeed * deltaTime;
            }

            return velocity;
        }
        
        private Vector3 AlignDirectionToCameraView(Vector3 direction) => 
            Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * direction;

        private void PeformJump()
        {
            _currentJumpTime = 0f;
            NotifyMovementPerforemed();
        }
    }
}