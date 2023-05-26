using System.Collections.Generic;
using Common.FSM;
using Gameplay.GroundTypeObserving;

namespace Gameplay.Movement.States.Base
{
    public abstract class ExitableMovementState : IExitableState
    {
        protected abstract HashSet<GroundType> AllowedBodyEnvironmentTypes { get; }

        public abstract void Exit();

        public bool IsWorkableWithBodyEnvironmentType(GroundType type) => 
            AllowedBodyEnvironmentTypes.Contains(type);

        public abstract void Dispose();
    }
}