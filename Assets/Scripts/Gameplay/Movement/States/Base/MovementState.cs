using Common.FSM;

namespace Gameplay.Movement.States.Base
{
    public abstract class MovementState : ExitableMovementState, IState
    {
        public abstract void Enter();
    }
}