using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public interface ISlopeCalculatorFactory
    {
        ISlopeCalculator Create(Transform parent, GameObject rayCastPointPrefab, float rayDistance);
    }
}