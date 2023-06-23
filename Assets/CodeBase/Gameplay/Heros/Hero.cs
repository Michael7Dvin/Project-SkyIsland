using System;
using Gameplay.Dying;
using Gameplay.Heros.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Heros
{
    public class Hero : IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly IHeroMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;
        private readonly IDeath _death;

        private readonly IDestroyable _destroyable;

        public Hero(GameObject gameObject,
            IHeroMovement movement,
            IInjuryProcessor injuryProcessor,
            IDeath death,
            IDestroyable destroyable)
        {
            _gameObject = gameObject;
            _movement = movement;
            _injuryProcessor = injuryProcessor;
            _death = death;

            _destroyable = destroyable;

            _death.Died += OnDied;
            _destroyable.Destroyed += Dispose;
        }

        private void OnDied() =>
            Object.Destroy(_gameObject);

        public void Dispose()
        {
            _movement.Dispose();
            _injuryProcessor.Dispose();
            _death.Dispose();

            _death.Died -= OnDied;
            _destroyable.Destroyed -= Dispose;
        }
    }
}