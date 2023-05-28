using System;
using Common.FSM;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;

namespace Gameplay.Movement
{
    public class MovementStateMachine : IDisposable
    {
        private readonly StateRunner<ExitableMovementState> _stateRunner = new();
        
        private readonly IMovementStateProvider _stateProvider;
        private readonly IGroundTypeTracker _groundTypeTracker;

        public MovementStateMachine(IMovementStateProvider stateProvider, IGroundTypeTracker groundTypeTracker)
        {
            _stateProvider = stateProvider;
            _groundTypeTracker = groundTypeTracker;

            _groundTypeTracker.CurrentGroundType.Changed += OnGroundTypeChanged;
            _stateRunner.ActiveState.Changed += OnActiveStateChanged;
            
            EnterDefaultState();
        }

        public ExitableMovementState ActiveState { get; private set; }

        private GroundType CurrentGroundType => _groundTypeTracker.CurrentGroundType.Value;

        public void Dispose()
        {
            _groundTypeTracker.CurrentGroundType.Changed -= OnGroundTypeChanged;
            _stateRunner.ActiveState.Changed -= OnActiveStateChanged;
        }

        private void OnActiveStateChanged(ExitableMovementState state)
        {
            if (ActiveState != null) 
                ActiveState.MovementPerformed -= OnStateMovementPerformed;
            
            ActiveState = state;
            ActiveState.MovementPerformed += OnStateMovementPerformed;
        }

        private void OnStateMovementPerformed() => 
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
            
            if (state.CanStartWithGroundType(CurrentGroundType)) 
                _stateRunner.EnterState(state);
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : MovementStateWithArguments<TArgs>
        {
            MovementStateWithArguments<TArgs> state = _stateProvider.GetState<TState, TArgs>();
            
            if (state.CanStartWithGroundType(CurrentGroundType)) 
                _stateRunner.EnterState(state, args);
        }
    }
}