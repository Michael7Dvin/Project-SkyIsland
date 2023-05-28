using Common.Observable;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Movement.SlopeCalculation;
using UnityEngine;

namespace Gameplay.Movement.GroundTypeTracking
{
    public class GroundTypeTracker : IGroundTypeTracker 
    {
        private readonly IGroundSpherecaster _groundSpherecaster;
        private readonly ISlopeCalculator _slopeCalculator;
        private readonly float _slopeMinAngle;

        private readonly Observable<GroundType> _currentGroundType = new();

        public GroundTypeTracker(IGroundSpherecaster groundSpherecaster,
            ISlopeCalculator slopeCalculator,
            float slopeMinAngle)
        {
            _groundSpherecaster = groundSpherecaster;
            _slopeCalculator = slopeCalculator;
            _slopeMinAngle = slopeMinAngle;

            _groundSpherecaster.SphereCasted += OnSphereCasted;
            _groundSpherecaster.SphereCastMissed += OnSphereCastMissed;
        }

        public IReadOnlyObservable<GroundType> CurrentGroundType => _currentGroundType;

        public void Dispose()
        {
            _groundSpherecaster.SphereCasted -= OnSphereCasted;
            _groundSpherecaster.SphereCastMissed -= OnSphereCastMissed;
        }

        private void OnSphereCasted(RaycastHit hit)
        {
            if (_slopeCalculator.SlopeAngle >= _slopeMinAngle)
            {
                _currentGroundType.Value = GroundType.Slope;
                return;   
            }

            _currentGroundType.Value = GroundType.Ground;
        }

        private void OnSphereCastMissed() => 
            _currentGroundType.Value = GroundType.Air;
    }
}