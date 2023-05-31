using System;
using System.Collections.Generic;
using Common.Observable;

namespace Common.FSM
{
    public class StateMachine<TBaseState> : IDisposable where TBaseState : IExitableState
    {
        private readonly Dictionary<Type, TBaseState> _states = new();
        private readonly Observable<TBaseState> _activeState = new();
        
        public IReadOnlyObservable<TBaseState> ActiveState => _activeState;

        public void Dispose() => 
            _activeState.Value?.Exit();

        public void EnterState<TState>() where TState : TBaseState, IState
        {
            TBaseState newState = _states[typeof(TState)];
        
            _activeState.Value?.Exit();
            _activeState.Value = newState;
            (newState as IState).Enter();
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : TBaseState, IStateWithArguments<TArgs>
        {
            TBaseState newState = _states[typeof(TState)];
        
            _activeState.Value?.Exit();
            _activeState.Value = newState;
            (newState as IStateWithArguments<TArgs>).Enter(args);
        }

        public void AddState<TState>(TState state) where TState : TBaseState
        {
            Type stateType = typeof(TState); 
        
            if (_states.ContainsKey(stateType) == true) 
                return;
    
            _states.Add(stateType, state);
        }
    }
}