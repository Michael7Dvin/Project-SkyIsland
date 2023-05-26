using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Gameplay.GroundTypeObserving
{
    public interface IGroundTypeObserverFactory
    {
        IGroundTypeObserver Create(Transform parent, CollisionObserver collisionObserverPrefab);
    }
}