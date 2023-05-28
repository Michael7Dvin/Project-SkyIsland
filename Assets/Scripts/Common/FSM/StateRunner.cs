using Common.Observable;

namespace Common.FSM
{
    public class StateRunner<TBaseState> where TBaseState : IExitableState
    {
        private readonly Observable<TBaseState> _activeState = new();
        
        public IReadOnlyObservable<TBaseState> ActiveState => _activeState;

        public void EnterState<TState>(TState state) where TState : TBaseState, IState
        {
            _activeState.Value?.Exit();
            _activeState.Value = state;
            state.Enter();
        }

        public void EnterState<TState, TArgs>(TState state, TArgs args) where TState : TBaseState, IStateWithArguments<TArgs>
        {
            _activeState.Value?.Exit();
            _activeState.Value = state;
            state.Enter(args);
        }
    }
}