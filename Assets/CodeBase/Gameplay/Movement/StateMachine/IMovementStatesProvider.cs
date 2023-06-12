using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.StateMachine.States.Base;

namespace Gameplay.Movement.StateMachine
{
    public interface IMovementStatesProvider
    {
        void AddState<TState>(TState state) where TState : ExitableMovementState;
        TState GetState<TState>() where TState : MovementState;
        TState GetState<TState, TArgs>() where TState : MovementStateWithArguments<TArgs>;
        MovementState GetDefaultStateByGroundType(GroundType groundType);
    }
}