using System.Collections.Generic;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class JogState : MovementState
    {
        private readonly float _speed;
        
        private readonly CharacterController _characterController;
        private readonly IUpdater _updater;
        private readonly IInputService _input;
        private readonly ICustomLogger _logger;

        public JogState(CharacterController characterController,
            IUpdater updater,
            IInputService input,

            ICustomLogger logger)
        {
            _characterController = characterController;
            _updater = updater;
            _input = input;
            _logger = logger;
        }

        protected override HashSet<BodyEnvironmentType> AllowedBodyEnvironmentTypes { get; } = new()
            {
                BodyEnvironmentType.Grounded
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
            if (_input.HorizontalDirection.Value != Vector2.zero)
            {
                Vector2 velocity = _input.HorizontalDirection.Value * _speed * deltaTime;
                _characterController.Move(new Vector3(velocity.x, 0f, velocity.y));
            }
        }
    }
}