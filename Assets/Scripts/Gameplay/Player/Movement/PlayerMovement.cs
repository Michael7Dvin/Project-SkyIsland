using System;
using Common.FSM;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private readonly StateMachine<ExitableMovementState> _stateMachine;

        private readonly IGroundTypeTracker _groundTracker;
        private readonly ISlopeCalculator _slopeCalculator;

        public PlayerMovement(StateMachine<ExitableMovementState> stateMachine,
            IGroundTypeTracker groundTracker,
            ISlopeCalculator slopeCalculator)
        {
            _stateMachine = stateMachine;
            
            _groundTracker = groundTracker;
            _slopeCalculator = slopeCalculator;

            _stateMachine.EnterState<FallState>();
            _groundTracker.CurrentGroundType.Changed += OnEnvironmentTypeChanged;
        }
        
        public void Dispose()
        {
            _groundTracker.CurrentGroundType.Changed -= OnEnvironmentTypeChanged;
            _stateMachine.Dispose();
            _slopeCalculator.Dispose();
        }
        
        private void OnEnvironmentTypeChanged(GroundType groundType)
        {
            if (_stateMachine.ActiveState.Value.IsWorkableWithBodyEnvironmentType(groundType) == false)
            {
                switch (groundType)
                {
                    case GroundType.Ground:
                        _stateMachine.EnterState<JogState>();
                        break;
                    case GroundType.Air:
                        _stateMachine.EnterState<FallState>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
                }    
            }
        }
    }
}