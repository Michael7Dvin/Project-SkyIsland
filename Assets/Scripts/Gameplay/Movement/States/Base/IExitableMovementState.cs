using Common.FSM;
using Gameplay.BodyEnvironmentObserving;

namespace Gameplay.Movement.States.Base
{
    public interface IExitableMovementState : IExitableState
    {
        bool IsWorkableWithBodyEnvironmentType(BodyEnvironmentType type);
    }
}