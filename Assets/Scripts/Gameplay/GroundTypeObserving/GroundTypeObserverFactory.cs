using Gameplay.MonoBehaviours;
using Infrastructure.Services.Configuration;
using UnityEngine;

namespace Gameplay.GroundTypeObserving
{
    public class GroundTypeObserverFactory : IGroundTypeObserverFactory
    {
        private readonly LayerMask _groundLayerMask;

        public GroundTypeObserverFactory(IConfigProvider configProvider)
        {
            _groundLayerMask = configProvider.GetForMovement().GroundLayerMask;
        }

        public IGroundTypeObserver Create(Transform parent, CollisionObserver collisionObserverPrefab)
        {
            CollisionObserver collisionObserver = Object.Instantiate(collisionObserverPrefab, parent);
            
            return new GroundTypeObserver(collisionObserver, _groundLayerMask);
        }
    }
}