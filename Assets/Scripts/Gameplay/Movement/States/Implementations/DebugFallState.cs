using System;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;

namespace Gameplay.Movement.States.Implementations
{
    public class DebugFallState : IMovementStateWithArguments<string>
    {
        private readonly float _speed;
        
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public DebugFallState(float speed, IUpdater updater, ICustomLogger logger)
        {
            _speed = speed;
            
            _updater = updater;
            _logger = logger;
        }

        public void Enter(string argument)
        {
            _logger.Log("Enter Fall State, argument: " + argument);
            _updater.Updated += Fall;
        }

        public void Exit()
        {
            _logger.Log("Exit Fall State");
            _updater.Updated -= Fall;
        }

        private void Fall(float deltaTime)
        {
            _logger.Log($"Fall: {_speed * deltaTime}m");
        }

        public bool IsWorkableWithBodyEnvironmentType(BodyEnvironmentType type)
        {
            switch (type)
            {
                case BodyEnvironmentType.Grounded:
                    return false;
                case BodyEnvironmentType.InAir:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}