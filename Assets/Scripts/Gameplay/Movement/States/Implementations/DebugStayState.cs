using System;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;

namespace Gameplay.Movement.States.Implementations
{
    public class DebugStayState : IMovementState
    {
        private readonly ICustomLogger _logger;

        public DebugStayState(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void Enter()
        {
            _logger.Log("Enter Stay State");
        }

        public void Exit()
        {
            _logger.Log("Exit Stay State");
        }

        public bool IsWorkableWithBodyEnvironmentType(BodyEnvironmentType type)
        {
            switch (type)
            {
                case BodyEnvironmentType.Grounded:
                    return true;
                case BodyEnvironmentType.InAir:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}