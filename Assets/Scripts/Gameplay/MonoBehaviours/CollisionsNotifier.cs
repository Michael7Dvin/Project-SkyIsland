using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionsNotifier : GameObjectLifeCycleNotifier, ICollisionNotifier
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionStayed;
        public event Action<Collision> CollisionExited;

        private void OnCollisionEnter(Collision collision) => 
            CollisionEntered?.Invoke(collision);

        private void OnCollisionStay(Collision collision) => 
            CollisionStayed?.Invoke(collision);

        private void OnCollisionExit(Collision collision) => 
            CollisionExited?.Invoke(collision);
    }
}