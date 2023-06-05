using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public class GroundSpherecasterFactory : IGroundSpherecasterFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IUpdater _updater;

        public GroundSpherecasterFactory(IInstantiator instantiator, IUpdater updater)
        {
            _instantiator = instantiator;
            _updater = updater;
        }

        public IGroundSpherecaster Create(Transform parent, GameObject rayCastPointPrefab, float sphereCastRadius, float sphereCastDistance)
        {
            GameObject rayCastPoint = _instantiator.Instantiate(rayCastPointPrefab, parent);
            
            return new GroundSpherecaster(_updater, rayCastPoint.transform, sphereCastRadius, sphereCastDistance);
        }
    }
}