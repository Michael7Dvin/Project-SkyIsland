using System;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public class GroundSphereCaster
    {
        private Transform _castPoint;
        private float _sphereRadius;
        private float _castDistance;
        
        private readonly IUpdater _updater;
        
        public GroundSphereCaster(IUpdater updater)
        {
            _updater = updater;
        }

        public void Construct(Transform castPoint, float sphereRadius, float castDistance)
        {
            _castPoint = castPoint;
            _sphereRadius = sphereRadius;
            _castDistance = castDistance;
            
            _updater.FixedUpdated += FixedUpdate;
        }

        public event Action<RaycastHit> SphereCasted;
        public event Action SphereCastMissed;

        public void Dispose() => 
            _updater.FixedUpdated -= FixedUpdate;

        private void FixedUpdate(float deltaTime)
        {
            if (Physics.SphereCast(_castPoint.position, _sphereRadius, Vector3.down, out RaycastHit hit, _castDistance))
                SphereCasted?.Invoke(hit);
            else
                SphereCastMissed?.Invoke();
        }
    }
}