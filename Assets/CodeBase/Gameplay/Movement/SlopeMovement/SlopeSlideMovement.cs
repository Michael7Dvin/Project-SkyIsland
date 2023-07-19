using Gameplay.Movement.SlopeCalculation;
using UnityEngine;

namespace Gameplay.Movement.SlopeMovement
{
    public class SlopeSlideMovement
    {
        private readonly float _speed;
        private readonly float _minSlopeAngle;
        
        private readonly SlopeCalculator _slopeCalculator;

        public SlopeSlideMovement(float speed, float minSlopeAngle, SlopeCalculator slopeCalculator)
        {
            _speed = speed;
            _minSlopeAngle = minSlopeAngle;
            _slopeCalculator = slopeCalculator;
        }

        public bool IsSteepSlope => _slopeCalculator.SlopeAngle >= _minSlopeAngle;
        
        public Vector3 GetSlideDownSlopeVelocity(float deltaTime) => 
            _slopeCalculator.SlopeDirection * -_speed * deltaTime;
    }
}