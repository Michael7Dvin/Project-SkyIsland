using Gameplay.Movement.SlopeCalculation;
using UnityEngine;

namespace Gameplay.Movement.SlopeMovement
{
    public class SlopeSlideMovement : ISlopeSlideMovement
    {
        private readonly float _speed;
        private readonly float _minSlopeAngle;
        
        private readonly ISlopeCalculator _slopeCalculator;

        public SlopeSlideMovement(float speed, float minSlopeAngle, ISlopeCalculator slopeCalculator)
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