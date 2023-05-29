using System;
using Gameplay.Movement.StateMachine.States.Base;

namespace Gameplay.Movement.StateMachine
{
    public interface IMovementStateMachine : IDisposable
    {
        ExitableMovementState ActiveState { get; }
        
        void EnterState<TState>() where TState : MovementState;
        void EnterState<TState, TArgs>(TArgs args) where TState : MovementStateWithArguments<TArgs>;
    }
}