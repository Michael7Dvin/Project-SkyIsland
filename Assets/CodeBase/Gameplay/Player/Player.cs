using System;
using Gameplay.Dying;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Player
{
    public class Player : IDisposable
    {
        private readonly GameObject _gameObject;
        
        private readonly IPlayerMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;
        private readonly IDeath _death;
        
        private readonly IGameObjectLifeCycleNotifier _gameObjectLifeCycleNotifier;

        public Player(GameObject gameObject,
            IPlayerMovement movement,
            IInjuryProcessor injuryProcessor,
            IDeath death,
            IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier)
        {
            _gameObject = gameObject;
            _movement = movement;
            _injuryProcessor = injuryProcessor;
            _death = death;

            _gameObjectLifeCycleNotifier = gameObjectLifeCycleNotifier;

            _death.Died += OnDied;
            _gameObjectLifeCycleNotifier.Destroyed += Dispose;
        }

        private void OnDied() =>
            Object.Destroy(_gameObject);

        public void Dispose()
        {
            _movement.Dispose();
            _injuryProcessor.Dispose();
            _death.Dispose();

            _death.Died -= OnDied;
            _gameObjectLifeCycleNotifier.Destroyed -= Dispose;
        }
    }
}