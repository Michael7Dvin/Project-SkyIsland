using System;
using Gameplay.Healths;
using Infrastructure.Services.Destroying;
using UnityEngine;

namespace Gameplay.Dying
{
    public class Death : IDeath
    {
        private IHealth _health;
        private GameObject _gameObject;
        private readonly IDestroyer _destroyer;

        public Death(IDestroyer destroyer)
        {
            _destroyer = destroyer;
        }

        public void Construct(IHealth health, GameObject gameObject)
        {
            _health = health;
            _gameObject = gameObject;
            
            _health.Current.Changed += OnHealthChanged;
        }
        
        public event Action Died;

        private void OnHealthChanged(float health)
        {
            if (health == 0)
                Die();
        }

        private void Die()
        {
            _destroyer.Destroy(_gameObject);
            Died?.Invoke();
            _health.Current.Changed -= OnHealthChanged;
        }
        
        public void Dispose() => 
            _health.Current.Changed -= OnHealthChanged;
    }
}