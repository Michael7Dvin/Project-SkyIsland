using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;

namespace Gameplay.Movement
{
    public interface IMovementStateProvider
    {
        void AddState<TState>(TState state) where TState : ExitableMovementState;
        TState GetState<TState>() where TState : MovementState;
        TState GetState<TState, TArgs>() where TState : MovementStateWithArguments<TArgs>;
        MovementState GetDefaultStateByGroundType(GroundType groundType);
    }
}