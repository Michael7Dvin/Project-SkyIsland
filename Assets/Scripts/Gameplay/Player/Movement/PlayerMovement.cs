using System;
using Common.FSM;
using Gameplay.GroundTypeObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private readonly StateMachine<ExitableMovementState> _stateMachine;
        private readonly IGroundTypeObserver _groundObserver;

        public PlayerMovement(StateMachine<ExitableMovementState> stateMachine,
            IGroundTypeObserver groundObserver)
        {
            _stateMachine = stateMachine;
            _groundObserver = groundObserver;
            
            _stateMachine.EnterState<FallState>();
            _groundObserver.CurrentGroundType.Changed += OnEnvironmentTypeChanged;
        }
        
        public void Dispose()
        {
            _groundObserver.CurrentGroundType.Changed -= OnEnvironmentTypeChanged;
            _stateMachine.Dispose();
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