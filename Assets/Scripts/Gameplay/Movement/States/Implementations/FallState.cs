using System.Collections.Generic;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;

namespace Gameplay.Movement.States.Implementations
{
    public class FallState : MovementState
    {
        private readonly float _speed;
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public FallState(float speed, IUpdater updater, ICustomLogger logger)
        {
            _speed = speed;
            _updater = updater;
            _logger = logger;
        }

        protected override HashSet<BodyEnvironmentType> AllowedBodyEnvironmentTypes { get; } = new()
        {
            BodyEnvironmentType.InAir,
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
        }
    }
}