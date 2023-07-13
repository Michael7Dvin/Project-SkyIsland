using System;
using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Heros.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Heros
{
    public class Hero : ISavableHero, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly CharacterController _characterController;
        private readonly IHealth _health;
        private readonly IHeroMovement _movement;
        private readonly IInjuryProcessor _injuryProcessor;
        private readonly IDeath _death;
        private readonly IDestroyable _destroyable;

        public Hero(GameObject gameObject,
            CharacterController characterController,
            IHeroMovement movement,
            IHealth health,
            IInjuryProcessor injuryProcessor,
            IDeath death,
            IDestroyable destroyable)
        {
            _gameObject = gameObject;
            _characterController = characterController;
            _movement = movement;
            _health = health;
            _injuryProcessor = injuryProcessor;
            _death = death;
            _destroyable = destroyable;

            _death.Died += OnDied;
            _destroyable.Destroyed += Dispose;
        }

        private void OnDied() =>
            Object.Destroy(_gameObject);
        
        public Vector3 Position
        {
            get => _gameObject.transform.position;
            set
            {
                _characterController.enabled = false;
                _gameObject.transform.position = value;
                _characterController.enabled = true;
            }
        }

        public Quaternion Rotation
        {
            get => _gameObject.transform.rotation;
            set => _gameObject.transform.rotation = value;
        }

        public float Health
        {
            get => _health.Current.Value;
            set => _health.SetCurrent(value);
        }

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