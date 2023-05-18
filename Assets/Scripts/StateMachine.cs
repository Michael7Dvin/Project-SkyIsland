using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly Dictionary<Type, IExitableState> _states = new();
    private IExitableState _activeState;

    public void EnterState<TState>() where TState : IExitableState
    {
        IExitableState newState = _states[typeof(TState)];
        
        _activeState?.Exit();
        _activeState = newState;
        (newState as IState).Enter();
    }
    
    public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState
    {
        IExitableState newState = _states[typeof(TState)];
        
        _activeState?.Exit();
        _activeState = newState;
        (newState as IStateWithArguments<TArgs>).Enter(args);
    }
    
    public void AddState<TState>(TState state) where TState : IExitableState
    {
        Type stateType = typeof(TState); 
        
        if (_states.ContainsKey(stateType) == true) 
            return;
    
        _states.Add(stateType, state);
    }
}