using System;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public class GroundSpherecaster : IGroundSpherecaster
    {
        private readonly IUpdater _updater;
        
        private readonly Transform _castPoint;
        private readonly float _sphereRadius;
        private readonly float _castDistance;
        
        public GroundSpherecaster(IUpdater updater, Transform castPoint, float sphereRadius, float castDistance)
        {
            _updater = updater;
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
            {
                SphereCasted?.Invoke(hit);
            }
            else
            {
                SphereCastMissed?.Invoke();
            }
        }
    }
}