using System.Collections.Generic;
using Gameplay.GroundTypeObserving;
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
        
        private readonly CharacterController _characterController;
        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;
        
        private readonly Transform _camera;

        public JogState(float jogSpeed,
            CharacterController characterController,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger,
            Transform camera)
        {
            _jogSpeed = jogSpeed;
            _characterController = characterController;
            _updater = updater;
            _input = input;
            _logger = logger;
            _camera = camera;
        }

        protected override HashSet<GroundType> AllowedBodyEnvironmentTypes { get; } = new()
            {
                GroundType.Ground
            };

        public override void Dispose() => 
            _updater.Updated -= Move;

        public override void Enter()
        {
            _logger.Log("Enter Jog State");
            _updater.Updated += Move;
        }

        public override void Exit()
        {
            _logger.Log("Exit Jog State");
            _updater.Updated -= Move;
        }

        private void Move(float deltaTime)
        {
            if (_input.HorizontalDirection.Value != Vector3.zero)
            {
                Vector3 alignedInputDirection = AlignInputDirectionToCameraView(_input.HorizontalDirection.Value);
                
                Vector3 velocity = alignedInputDirection * _jogSpeed * deltaTime;
                _characterController.Move(velocity);
            }

            _characterController.Move(new Vector3(0f, -4.5f * deltaTime, 0f));
        }

        private Vector3 AlignInputDirectionToCameraView(Vector3 inputDirection) => 
            Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * inputDirection;
    }
}