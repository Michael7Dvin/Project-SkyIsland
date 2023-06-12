using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public interface ICollisionNotifier : IDestroyable
    {
        event Action<Collision> CollisionEntered;
        event Action<Collision> CollisionStayed;
        event Action<Collision> CollisionExited;
    }
}