using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class GameObjectLifeCycleNotifier : MonoBehaviour, IGameObjectLifeCycleNotifier
    {
        public event Action Destroyed;

        private void OnDestroy() => 
            Destroyed?.Invoke();
    }
}