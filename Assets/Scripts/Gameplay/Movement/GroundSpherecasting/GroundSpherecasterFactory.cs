using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public class GroundSpherecasterFactory : IGroundSpherecasterFactory 
    {
        private readonly IUpdater _updater;

        public GroundSpherecasterFactory(IUpdater updater)
        {
            _updater = updater;
        }

        public IGroundSpherecaster Create(Transform parent, GameObject rayCastPointPrefab, float sphereCastRadius, float sphereCastDistance)
        {
            GameObject rayCastPoint = Object.Instantiate(rayCastPointPrefab, parent);
            
            return new GroundSpherecaster(_updater, rayCastPoint.transform, sphereCastRadius, sphereCastDistance);
        }
    }
}