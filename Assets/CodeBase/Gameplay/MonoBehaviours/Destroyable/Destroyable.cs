using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours.Destroyable
{
    public class Destroyable : MonoBehaviour, IDestroyable
    {
        public event Action Destroyed;

        private void OnDestroy() => 
            Destroyed?.Invoke();
    }
}