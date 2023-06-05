using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public interface ICollisionNotifier : IGameObjectLifeCycleNotifier
    {
        event Action<Collision> CollisionEntered;
        event Action<Collision> CollisionStayed;
        event Action<Collision> CollisionExited;
    }
}