using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours.Collidable
{
    [RequireComponent(typeof(Rigidbody))]
    public class Collidable : Destroyable.Destroyable, ICollisionNotifier
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