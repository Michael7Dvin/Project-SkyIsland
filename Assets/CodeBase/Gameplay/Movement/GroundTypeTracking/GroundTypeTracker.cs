﻿using Common.Observable;
using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Movement.GroundTypeTracking
{
    public class GroundTypeTracker : IGroundTypeTracker
    {
        private readonly IGroundSphereCaster _groundSphereCaster;

        private readonly Observable<GroundType> _currentGroundType = new();

        public GroundTypeTracker(IGroundSphereCaster groundSphereCaster)
        {
            _groundSphereCaster = groundSphereCaster;
            
            _groundSphereCaster.SphereCasted += OnSphereCasted;
            _groundSphereCaster.SphereCastMissed += OnSphereCastMissed;
        }

        public IReadOnlyObservable<GroundType> CurrentGroundType => _currentGroundType;

        public void Dispose()
        {
            _groundSphereCaster.SphereCasted -= OnSphereCasted;
            _groundSphereCaster.SphereCastMissed -= OnSphereCastMissed;
        }

        private void OnSphereCasted(RaycastHit hit) => 
            _currentGroundType.Value = GroundType.Ground;

        private void OnSphereCastMissed() =>
            _currentGroundType.Value = GroundType.Air;

    }
}