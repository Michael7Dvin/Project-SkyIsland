using System;
using UnityEngine;

namespace Gameplay
{
    public class GameObjectLifeCycleNotifier : MonoBehaviour, IGameObjectLifeCycleNotifier
    {
        public event Action Awoke;
        public event Action Started;
        public event Action Enabled;
        public event Action Disabled;
        public event Action Destroyed;
        
        private void Awake() => 
            Awoke?.Invoke();
        
        private void Start() => 
            Started?.Invoke();

        private void OnEnable() => 
            Enabled?.Invoke();

        private void OnDisable() => 
            Disabled?.Invoke();

        private void OnDestroy() => 
            Destroyed?.Invoke();
    }
}