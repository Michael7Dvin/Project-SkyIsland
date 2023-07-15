using System;
using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Heros.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;

namespace Gameplay.Heros
{
    public class Hero : IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly IHealth _health;
        private readonly IMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;

        public Hero(GameObject gameObject,
            IMovement movement,
            IHealth health,
            IInjuryProcessor injuryProcessor,
            IDeath death,
            IDestroyable destroyable,
            IHeroProgressDataProvider heroProgressDataProvider)
        {
            _gameObject = gameObject;
            _movement = movement;
            _health = health;
            _injuryProcessor = injuryProcessor;
            Death = death;
            
            Destroyable = destroyable;
            HeroProgressDataProvider = heroProgressDataProvider;

            Destroyable.Destroyed += Dispose;
        }

        public IDeath Death { get; set; }
        public IDestroyable Destroyable { get; }
        public IHeroProgressDataProvider HeroProgressDataProvider { get; }

        public void Dispose()
        {
            _movement.Dispose();
            _injuryProcessor.Dispose();
            Death.Dispose();

            Destroyable.Destroyed -= Dispose;
        }
    }
}