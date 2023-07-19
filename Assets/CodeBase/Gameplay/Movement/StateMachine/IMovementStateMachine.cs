using System;
using Common.Observable;
using Gameplay.Movement.StateMachine.States.Base;

namespace Gameplay.Movement.StateMachine
{
    public interface IMovementStateMachine : IDisposable
    {
        IReadOnlyObservable<ExitableMovementState> ActiveState { get; }
        
        void EnterState<TState>() where TState : MovementState;
        void EnterState<TState, TArgs>(TArgs args) where TState : MovementStateWithArgument<TArgs>;
    }
}