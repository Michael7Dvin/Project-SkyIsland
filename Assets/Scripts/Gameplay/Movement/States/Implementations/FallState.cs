using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class FallState : MovementState
    {
        private readonly float _verticalSpeed;
        private readonly float _horizontalspeed;
        
        private readonly Transform _camera;

        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;

        public FallState(float verticalSpeed,
            float horizontalspeed,
            Transform camera,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger)
        {
            _verticalSpeed = verticalSpeed;
            _horizontalspeed = horizontalspeed;
            
            _camera = camera;

            _updater = updater;
            _input = input;
            _logger = logger;
        }

        protected override HashSet<GroundType> AllowedGroundTypes { get; } = new()
        {
            GroundType.Air,
        };

        public override void Dispose() => 
            _updater.Updated -= Update;

        private void Update(float deltaTime)
        {
            MoveVelocity = GetFallVelocity(deltaTime);
        }
        
        public override void Enter()
        {
            _logger.Log("Enter Fall State");
            _updater.Updated += Update;
        }

        public override void Exit()
        {
            _logger.Log("Exit Fall State");
            _updater.Updated -= Update;
        }
        
        private Vector3 GetFallVelocity(float deltaTime)
        {
            Vector3 velocity = new();
            
            if (_input.HorizontalDirection.Value != Vector3.zero)
            {
                Vector3 cameraAlignedDirection = AlignDirectionToCameraView(_input.HorizontalDirection.Value);
                velocity = cameraAlignedDirection * _horizontalspeed * deltaTime;
            }
            
            return velocity + Vector3.down * _verticalSpeed * deltaTime;
        }

        private Vector3 AlignDirectionToCameraView(Vector3 direction) => 
            Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * direction;
    }
}