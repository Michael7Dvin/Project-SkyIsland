using System;
using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.StateMachine.States.Base;
using Infrastructure.Services.Logger;

namespace Gameplay.Movement.StateMachine
{
    public class MovementStateProvider : IMovementStateProvider
    {
        private readonly Dictionary<Type, ExitableMovementState> _states = new();
        
        private MovementState _defaultStateWhileGround;
        private MovementState _defaultStateWhileAir;

        private readonly ICustomLogger _logger;

        public MovementStateProvider(MovementState defaultStateWhileGround,
            MovementState defaultStateWhileAir,
            ICustomLogger logger)
        {
            _logger = logger;
            
            ValidateAndSetDefaultStates(defaultStateWhileGround, defaultStateWhileAir);
        }

        public void AddState<TState>(TState state) where TState : ExitableMovementState
        {
            Type stateType = typeof(TState);

            if (_states.ContainsKey(stateType))
            {
                _logger.LogError($"State of type {stateType.Name} already exists in the repository.");
                return;
            }

            _states.Add(stateType, state);
        }

        public TState GetState<TState>() where TState : MovementState
        {
            Type stateType = typeof(TState);

            if (_states.TryGetValue(stateType, out ExitableMovementState state))
                return (TState)state;

            _logger.LogError($"State of type {stateType} is not found.");
            return null;
        }

        public TState GetState<TState, TArgs>() where TState : MovementStateWithArguments<TArgs>
        {
            Type stateType = typeof(TState);

            if (_states.TryGetValue(stateType, out ExitableMovementState state))
                return (TState)state;

            _logger.LogError($"State of type {stateType} is not found.");
            return null;
        }

        public MovementState GetDefaultStateByGroundType(GroundType groundType)
        {
            switch (groundType)
            {
                case GroundType.Ground:
                    return _defaultStateWhileGround;
                case GroundType.Air:
                    return _defaultStateWhileAir;
                default:
                    throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
            }
        }

        private void ValidateAndSetDefaultStates(MovementState defaultWhileGround, MovementState defaultWhileAir)
        {
            ValidateAndSetDefaultState(defaultWhileGround, GroundType.Ground, ref _defaultStateWhileGround);
            ValidateAndSetDefaultState(defaultWhileAir, GroundType.Air, ref _defaultStateWhileAir);
        }

        private void ValidateAndSetDefaultState(MovementState state,
            GroundType expectedGroundType,
            ref MovementState defaultState)
        {
            if (state.CanStart(expectedGroundType) && state.CanWorkWithGroundType(expectedGroundType))
                defaultState = state;
            else
                _logger.LogError($"Invalid default state for {expectedGroundType}.");
        }
    }
}