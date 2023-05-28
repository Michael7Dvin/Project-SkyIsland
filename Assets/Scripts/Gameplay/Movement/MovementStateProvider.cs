using System;
using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Logger;

namespace Gameplay.Movement
{
    public class MovementStateProvider : IMovementStateProvider
    {
        private readonly Dictionary<Type, ExitableMovementState> _states = new();
        
        private MovementState _defaultStateWhileGround;
        private MovementState _defaultStateWhileAir;
        private MovementState _defaultStateWhileSlope;

        private readonly ICustomLogger _logger;

        public MovementStateProvider(MovementState defaultStateWhileGround,
            MovementState defaultStateWhileAir,
            MovementState defaultStateWhileSlope,
            ICustomLogger logger)
        {
            _logger = logger;
            
            ValidateAndSetDefaultStates(defaultStateWhileGround, defaultStateWhileAir, defaultStateWhileSlope);
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
                case GroundType.Slope:
                    return _defaultStateWhileSlope;
                default:
                    throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
            }
        }

        private void ValidateAndSetDefaultStates(MovementState defaultStateWhileGround,
            MovementState defaultStateWhileAir,
            MovementState defaultStateWhileSlope)
        {
            ValidateAndSetDefaultState(defaultStateWhileGround, GroundType.Ground, ref _defaultStateWhileGround);
            ValidateAndSetDefaultState(defaultStateWhileAir, GroundType.Air, ref _defaultStateWhileAir);
            ValidateAndSetDefaultState(defaultStateWhileSlope, GroundType.Slope, ref _defaultStateWhileSlope);
        }

        private void ValidateAndSetDefaultState(MovementState state,
            GroundType expectedGroundType,
            ref MovementState defaultState)
        {
            if (state.CanStartWithGroundType(expectedGroundType) && state.CanWorkWithGroundType(expectedGroundType))
                defaultState = state;
            else
                _logger.LogError($"Invalid default state for {expectedGroundType}.");
        }
    }
}