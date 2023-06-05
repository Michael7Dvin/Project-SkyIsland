using Common.FSM;

namespace Gameplay.Movement.StateMachine.States.Base
{
    public abstract class MovementStateWithArguments<TArgs> : ExitableMovementState, IStateWithArguments<TArgs>
    {
        public abstract void Enter(TArgs argument);
    }
}