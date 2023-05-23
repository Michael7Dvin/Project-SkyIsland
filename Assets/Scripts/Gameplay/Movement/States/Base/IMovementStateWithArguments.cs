using Common.FSM;

namespace Gameplay.Movement.States.Base
{
    public interface IMovementStateWithArguments<in TArgs> : IExitableMovementState, IStateWithArguments<TArgs>
    {
    }
}