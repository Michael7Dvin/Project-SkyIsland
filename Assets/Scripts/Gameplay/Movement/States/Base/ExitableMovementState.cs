using System.Collections.Generic;
using Common.FSM;
using Gameplay.BodyEnvironmentObserving;

namespace Gameplay.Movement.States.Base
{
    public abstract class ExitableMovementState : IExitableState
    {
        protected abstract HashSet<BodyEnvironmentType> AllowedBodyEnvironmentTypes { get; }

        public abstract void Exit();

        public bool IsWorkableWithBodyEnvironmentType(BodyEnvironmentType type) => 
            AllowedBodyEnvironmentTypes.Contains(type);

        public abstract void Dispose();
    }
}