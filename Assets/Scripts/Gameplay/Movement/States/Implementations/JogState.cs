using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
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
        private readonly ISlopeCalculator _slopeCalculator;
        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;
        
        private readonly Transform _camera;

        public JogState(float jogSpeed,
            CharacterController characterController,
            ISlopeCalculator slopeCalculator,
            IUpdater updater,
            IInputService input,
            ICustomLogger logger,
            Transform camera)
        {
            _jogSpeed = jogSpeed;
            _characterController = characterController;
            _slopeCalculator = slopeCalculator;
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
            _updater.Updated -= MoveHorizontally;

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

        private void Update(float deltaTime)
        {
            MoveHorizontally(deltaTime);
            SlideDownSlope(deltaTime);
        }
        
        private void MoveHorizontally(float deltaTime)
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

        private void SlideDownSlope(float deltaTime)
        {
            if (_slopeCalculator.SlopeAngle >= _characterController.slopeLimit)
            {
                Vector3 velocity = _slopeCalculator.SlopeDirection * -5f * deltaTime;
                _characterController.Move(velocity);
            }
        }
    }
}