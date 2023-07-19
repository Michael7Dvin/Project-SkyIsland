﻿using System;
using Common.Observable;

namespace Common.FSM
{
    public class StateRunner<TBaseState> : IDisposable where TBaseState : IExitableState
    {
        private readonly Observable<TBaseState> _activeState = new();

        public IReadOnlyObservable<TBaseState> ActiveState => _activeState;

        public void Dispose() => 
            _activeState.Value?.Exit();

        public void EnterState<TState>(TState state) where TState : TBaseState, IState
        {
            _activeState.Value?.Exit();
            _activeState.Value = state;
            state.Enter();
        }

        public void EnterState<TState, TArgument>(TState state, TArgument args)
            where TState : TBaseState, IStateWithArgument<TArgument>
        {
            _activeState.Value?.Exit();
            _activeState.Value = state;
            state.Enter(args);
        }
    }
}