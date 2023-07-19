using System;
using System.Collections.Generic;

namespace Common.FSM
{
    public class StateMachine<TBaseState> : IDisposable where TBaseState : IExitableState
    {
        private readonly Dictionary<Type, TBaseState> _states = new();
        private TBaseState _activeState;

        public void Dispose() => 
            _activeState?.Exit();

        public void EnterState<TState>() where TState : TBaseState, IState
        {
            TBaseState newState = _states[typeof(TState)];
        
            _activeState?.Exit();
            _activeState = newState;
            (newState as IState).Enter();
        }

        public void EnterState<TState, TArgument>(TArgument argument)
            where TState : TBaseState, IStateWithArgument<TArgument>
        {
            TBaseState newState = _states[typeof(TState)];
        
            _activeState?.Exit();
            _activeState = newState;
            (newState as IStateWithArgument<TArgument>).Enter(argument);
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