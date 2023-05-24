using System;
using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement : IPlayerMovement
    {
        private readonly StateMachine<ExitableMovementState> _stateMachine;
        private readonly IBodyEnvironmentObserver _bodyEnvironmentObserver;

        public PlayerMovement(StateMachine<ExitableMovementState> stateMachine,
            IBodyEnvironmentObserver bodyEnvironmentObserver)
        {
            _stateMachine = stateMachine;
            _bodyEnvironmentObserver = bodyEnvironmentObserver;
            
            _stateMachine.EnterState<FallState>();
            _bodyEnvironmentObserver.EnvironmentType.Changed += OnEnvironmentTypeChanged;
        }
        
        public void Dispose()
        {
            _bodyEnvironmentObserver.EnvironmentType.Changed -= OnEnvironmentTypeChanged;
            _stateMachine.Dispose();
        }
        
        private void OnEnvironmentTypeChanged(BodyEnvironmentType environmentType)
        {
            if (_stateMachine.ActiveState.Value.IsWorkableWithBodyEnvironmentType(environmentType) == false)
            {
                switch (environmentType)
                {
                    case BodyEnvironmentType.Grounded:
                        _stateMachine.EnterState<StayState>();
                        break;
                    case BodyEnvironmentType.InAir:
                        _stateMachine.EnterState<FallState>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
                }    
            }
        }
    }
}