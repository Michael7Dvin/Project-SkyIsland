using System;
using Common.FSM;
using Gameplay.BodyEnvironmentObserving;
using Gameplay.Movement.States.Base;
using Gameplay.Movement.States.Implementations;
using Infrastructure.Services.Logger;
using Infrastructure.Services.Updater;

namespace Gameplay.Player.Movement
{
    public class PlayerMovement
    {
        public readonly BodyEnvironmentObserver BodyEnvironmentObserverPublicForDebug;

        private const string FallStateArgument = "FALL";

        private readonly StateMachine<IExitableMovementState> _stateMachine;

        public PlayerMovement(IUpdater updater, ICustomLogger logger)
        {
            BodyEnvironmentObserverPublicForDebug = new BodyEnvironmentObserver();

            _stateMachine = new StateMachine<IExitableMovementState>();
            _stateMachine.AddState(new DebugStayState(logger));
            _stateMachine.AddState(new DebugFallState(-5f, updater, logger));

            BodyEnvironmentObserverPublicForDebug.EnvironmentType.Changed += OnEnvironmentTypeChanged;
        }

        public void EnterStateForDebug<TState>() where TState : IMovementState => 
            _stateMachine.EnterState<TState>();

        public void Dispose()
        {
            BodyEnvironmentObserverPublicForDebug.EnvironmentType.Changed -= OnEnvironmentTypeChanged;
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
                        _stateMachine.EnterState<DebugFallState, string>(FallStateArgument);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
                }    
            }
        }
    }
}