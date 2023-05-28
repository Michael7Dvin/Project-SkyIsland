using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeCalculation;
using Gameplay.Movement.States.Base;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class SlopeSlideState : MovementState
    {
        private readonly float _slideSpeed;
        
        private readonly ISlopeCalculator _slopeCalculator;

        public SlopeSlideState(float slideSpeed, ISlopeCalculator slopeCalculator)
        {
            _slideSpeed = slideSpeed;
            _slopeCalculator = slopeCalculator;
        }

        protected override HashSet<GroundType> CanStartWithGroundTypes { get; } = new()
        {
            GroundType.Slope,
        };
        
        protected override HashSet<GroundType> CanWorkWithGroundTypes { get; } = new()
        {
            GroundType.Slope,
        };

        public override void Dispose()
        {
        }

        public override void Enter()
        {
        }
        
        public override void Exit()
        {
        }

        public override Vector3 GetMoveVelocty(float deltaTime)
        {
            Vector3 velocity = GetSlideDownSlopeVelocity(deltaTime);
            return velocity;
        }

        private Vector3 GetSlideDownSlopeVelocity(float deltaTime) => 
            _slopeCalculator.SlopeDirection * -_slideSpeed * deltaTime;
    }
}