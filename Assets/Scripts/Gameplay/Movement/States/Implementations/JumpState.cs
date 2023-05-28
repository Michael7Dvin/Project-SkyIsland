using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
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

        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;

        public JumpState(AnimationCurve jumpCurve,
            float horizontalspeed,
            Transform camera,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger)
        {
            _jumpCurve = jumpCurve;
            _horizontalspeed = horizontalspeed;
            
            _camera = camera;
            
            _updater = updater;
            _input = input;
            _logger = logger;

            SetTotalTime();
        }

        protected override HashSet<GroundType> AllowedGroundTypes { get; } = new()
        {
            GroundType.Ground,
            GroundType.Air,
        };

        public override void Dispose() => 
            _updater.Updated -= Update;

        private void Update(float deltaTime)
        {
            Vector3 velocity = GetJumpVelocity(deltaTime) + GetHorizontalMoveVelocity(deltaTime);
            MoveVelocity = velocity;
        }

        public override void Enter()
        {
            _logger.Log("Enter Jump State");
            _updater.Updated += Update;
        }

        public override void Exit()
        {
            _logger.Log("Exit Jump State");
            _updater.Updated -= Update;
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
                Debug.Log("total");
                _currentJumpTime = 0f;
                Complete();
            }

            Vector3 velocity = Vector3.up * verticalSpeed * deltaTime;
            return velocity;
        }

        private Vector3 GetHorizontalMoveVelocity(float deltaTime)
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