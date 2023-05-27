using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public interface IGroundSpherecasterFactory
    {
        IGroundSpherecaster Create(Transform parent, GameObject сastPointPrefab, float sphereCastRadius, float sphereCastDistance);
    }
}