using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public class SlopeCalculatorFactory : ISlopeCalculatorFactory 
    {
        private readonly IUpdater _updater;

        public SlopeCalculatorFactory(IUpdater updater)
        {
            _updater = updater;
        }

        public ISlopeCalculator Create(Transform parent, GameObject rayCastPointPrefab, float sphereCastRadius, float sphereCastDistance)
        {
            GameObject rayCastPoint = Object.Instantiate(rayCastPointPrefab, parent);
            
            return new SlopeCalculator(_updater, rayCastPoint.transform, sphereCastRadius, sphereCastDistance);
        }
    }
}