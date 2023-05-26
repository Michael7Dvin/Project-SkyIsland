using System.Collections.Generic;
using Gameplay.GroundTypeObserving;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;

namespace Gameplay.Movement.States.Implementations
{
    public class StayState : MovementState
    {
        private readonly ICustomLogger _logger;

        public StayState(ICustomLogger logger)
        {
            _logger = logger;
        }

        protected override HashSet<GroundType> AllowedBodyEnvironmentTypes { get; } = new()
        {
            GroundType.Ground,
        };

        public override void Dispose()
        {
        }

        public override void Enter() => 
            _logger.Log("Enter Stay State");

        public override void Exit() => 
            _logger.Log("Exit Stay State");
    }
}