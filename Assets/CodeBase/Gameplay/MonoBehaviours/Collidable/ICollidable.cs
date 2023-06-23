using System;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;

namespace Gameplay.MonoBehaviours.Collidable
{
    public interface ICollisionNotifier : IDestroyable
    {
        event Action<Collision> CollisionEntered;
        event Action<Collision> CollisionStayed;
        event Action<Collision> CollisionExited;
    }
}