using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class JogState : MovementState
    {
        private readonly float _jogSpeed;
        private readonly float _antiBumpSpeed;

        private readonly Transform _camera;
        
        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;
        
        public JogState(float jogSpeed,
            float antiBumpSpeed,
            Transform camera,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger)
        {
            _jogSpeed = jogSpeed;
            _antiBumpSpeed = antiBumpSpeed;

            _camera = camera;
            
            _updater = updater;
            _input = input;
            _logger = logger;
        }

        protected override HashSet<GroundType> AllowedGroundTypes { get; } = new()
            {
                GroundType.Ground
            };

        public override void Dispose() => 
            _updater.Updated -= Update;

        public override void Enter()
        {
            _logger.Log("Enter Jog State");
            _updater.Updated += Update;
        }

        public override void Exit()
        {
            _logger.Log("Exit Jog State");
            _updater.Updated -= Update;
        }

        private void Update(float deltaTime) => 
            MoveVelocity = GetJogVelocity(deltaTime) + GetAntiBumpVelocity(deltaTime);

        private Vector3 GetJogVelocity(float deltaTime)
        {
            if (_input.HorizontalDirection.Value != Vector3.zero)
            {
                Vector3 cameraAlignedDirection = AlignDirectionToCameraView(_input.HorizontalDirection.Value);
                return cameraAlignedDirection * _jogSpeed * deltaTime;
            }
            
            return Vector3.zero;
        }

        private Vector3 AlignDirectionToCameraView(Vector3 direction) => 
            Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * direction;

        private Vector3 GetAntiBumpVelocity(float deltaTime) => 
            Vector3.down * _antiBumpSpeed * deltaTime;
    }
}