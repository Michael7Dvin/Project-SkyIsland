using Common.FSM;
using Common.Observable;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.StateMachine.States.Base;

namespace Gameplay.Movement.StateMachine
{
    public class MovementStateMachine : IMovementStateMachine
    {
        private readonly StateRunner<ExitableMovementState> _stateRunner = new();
        
        private readonly IMovementStateProvider _stateProvider;
        private readonly IGroundTypeTracker _groundTypeTracker;

        public MovementStateMachine(IMovementStateProvider stateProvider, IGroundTypeTracker groundTypeTracker)
        {
            _stateProvider = stateProvider;
            _groundTypeTracker = groundTypeTracker;

            _groundTypeTracker.CurrentGroundType.Changed += OnGroundTypeChanged;
            EnterDefaultState();
        }

        public IReadOnlyObservable<ExitableMovementState> ActiveState => _stateRunner.ActiveState;

        private GroundType CurrentGroundType => _groundTypeTracker.CurrentGroundType.Value;

        public void Dispose() => 
            _groundTypeTracker.CurrentGroundType.Changed -= OnGroundTypeChanged;

        private void OnStatePerformed() => 
            EnterDefaultState();

        private void OnGroundTypeChanged(GroundType groundType)
        {
            if (_stateRunner.ActiveState.Value.CanWorkWithGroundType(groundType) == false)
                EnterDefaultState();
        }

        private void EnterDefaultState()
        {
            MovementState defaultState = _stateProvider.GetDefaultStateByGroundType(CurrentGroundType);
            _stateRunner.EnterState(defaultState);
        }

        public void EnterState<TState>() where TState : MovementState
        {
            MovementState state = _stateProvider.GetState<TState>();

            if (state.CanStartWithGroundType(CurrentGroundType) == true)
            {
                if (ActiveState.Value != null) 
                    ActiveState.Value.MovementPerformed -= OnStatePerformed;
                
                state.MovementPerformed += OnStatePerformed;
                
                _stateRunner.EnterState(state);
            }
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : MovementStateWithArguments<TArgs>
        {
            MovementStateWithArguments<TArgs> state = _stateProvider.GetState<TState, TArgs>();

            if (state.CanStartWithGroundType(CurrentGroundType) == true)
            {
                if (ActiveState.Value != null) 
                    ActiveState.Value.MovementPerformed -= OnStatePerformed;
                
                state.MovementPerformed += OnStatePerformed;
                
                _stateRunner.EnterState(state, args);
            }
        }
    }
}