using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class JumpState : MovementState
    {
        private float _currentJumpTime;
        private float _totalJumpTime;
        
        private readonly AnimationCurve _jumpCurve;
        private readonly float _horizontalspeed;
        
        private readonly Transform _camera;

        private readonly IInputService _input;

        public JumpState(AnimationCurve jumpCurve,
            float horizontalspeed,
            Transform camera,
            IInputService input)
        {
            _jumpCurve = jumpCurve;
            _horizontalspeed = horizontalspeed;
            
            _camera = camera;

            _input = input;

            SetTotalTime();
        }

        protected override HashSet<GroundType> CanStartWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };

        protected override HashSet<GroundType> CanWorkWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
            GroundType.Air,
        };

        public override void Dispose()
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override Vector3 GetMoveVelocty(float deltaTime)
        {
            Vector3 velocity = GetJumpVelocity(deltaTime) + GetHorizontalVelocity(deltaTime);
            return velocity;
        }

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
            {
                _currentJumpTime = 0f;
                NotifyMovementPerforemed();
            }

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
    }
}