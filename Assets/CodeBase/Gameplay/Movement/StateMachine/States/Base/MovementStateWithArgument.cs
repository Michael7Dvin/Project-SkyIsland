using Common.FSM;

namespace Gameplay.Movement.StateMachine.States.Base
{
    public abstract class MovementStateWithArgument<TArgs> : ExitableMovementState, IStateWithArgument<TArgs>
    {
        public abstract void Enter(TArgs argument);
    }
}