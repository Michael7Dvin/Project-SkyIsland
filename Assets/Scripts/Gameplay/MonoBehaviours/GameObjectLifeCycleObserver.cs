using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class GameObjectLifeCycleObserver : MonoBehaviour, IGameObjectLifeCycleObserver
    {
        public event Action Destroyed;

        private void OnDestroy() => 
            Destroyed?.Invoke();
    }
}