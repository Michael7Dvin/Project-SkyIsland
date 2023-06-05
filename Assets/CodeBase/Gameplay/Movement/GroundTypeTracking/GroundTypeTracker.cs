using Common.Observable;
using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Movement.GroundTypeTracking
{
    public class GroundTypeTracker : IGroundTypeTracker
    {
        private readonly IGroundSpherecaster _groundSpherecaster;

        private readonly Observable<GroundType> _currentGroundType = new();

        public GroundTypeTracker(IGroundSpherecaster groundSpherecaster)
        {
            _groundSpherecaster = groundSpherecaster;
            
            _groundSpherecaster.SphereCasted += OnSphereCasted;
            _groundSpherecaster.SphereCastMissed += OnSphereCastMissed;
        }

        public IReadOnlyObservable<GroundType> CurrentGroundType => _currentGroundType;

        public void Dispose()
        {
            _groundSpherecaster.SphereCasted -= OnSphereCasted;
            _groundSpherecaster.SphereCastMissed -= OnSphereCastMissed;
        }

        private void OnSphereCasted(RaycastHit hit) => 
            _currentGroundType.Value = GroundType.Ground;

        private void OnSphereCastMissed() =>
            _currentGroundType.Value = GroundType.Air;

    }
}