using System.Collections.Generic;
using Gameplay.GroundTypeObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class FallState : MovementState
    {
        private readonly float _fallSpeed;
        private readonly CharacterController _characterController;
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public FallState(float fallSpeed, CharacterController characterController, IUpdater updater, ICustomLogger logger)
        {
            _fallSpeed = fallSpeed;
            _characterController = characterController;
            _updater = updater;
            _logger = logger;
        }

        protected override HashSet<GroundType> AllowedBodyEnvironmentTypes { get; } = new()
        {
            GroundType.Air,
        };

        public override void Dispose() => 
            _updater.Updated -= Fall;

        public override void Enter()
        {
            _logger.Log("Enter Fall State");
            _updater.Updated += Fall;
        }

        public override void Exit()
        {
            _logger.Log("Exit Fall State");
            _updater.Updated -= Fall;
        }

        private void Fall(float deltaTime)
        {
            Vector3 velocity = new Vector3(0f, _fallSpeed * deltaTime, 0f);
            _characterController.Move(velocity);
        }
    }
}