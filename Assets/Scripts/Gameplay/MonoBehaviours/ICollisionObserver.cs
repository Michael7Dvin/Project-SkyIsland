using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public interface ICollisionObserver : IGameObjectLifeCycleObserver
    {
        event Action<Collision> CollisionEntered;
        event Action<Collision> CollisionStayed;
        event Action<Collision> CollisionExited;
    }
}