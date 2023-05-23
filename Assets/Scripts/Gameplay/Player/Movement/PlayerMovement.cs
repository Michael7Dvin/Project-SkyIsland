using System;
using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement
    {
        private readonly StateMachine<IExitableMovementState> _stateMachine;
        private readonly IBodyEnvironmentObserver _bodyEnvironmentObserver;

        public PlayerMovement(StateMachine<IExitableMovementState> stateMachine,
            IBodyEnvironmentObserver bodyEnvironmentObserver)
        {
            _stateMachine = stateMachine;
            _bodyEnvironmentObserver = bodyEnvironmentObserver;
            
            _bodyEnvironmentObserver.EnvironmentType.Changed += OnEnvironmentTypeChanged;
        }
        
        public void Dispose()
        {
            _bodyEnvironmentObserver.EnvironmentType.Changed -= OnEnvironmentTypeChanged;
        }
        
        private void OnEnvironmentTypeChanged(BodyEnvironmentType environmentType)
        {
            if (_stateMachine.ActiveState.Value.IsWorkableWithBodyEnvironmentType(environmentType) == false)
            {
                switch (environmentType)
                {
                    case BodyEnvironmentType.Grounded:
                        _stateMachine.EnterState<DebugStayState>();
                        break;
                    case BodyEnvironmentType.InAir:
                        _stateMachine.EnterState<DebugFallState>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
                }    
            }
        }
    }
}