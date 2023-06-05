using Gameplay.Healths;
using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Gameplay.InjuryProcessing
{
    public class InjuryProcessor : IInjuryProcessor
    {
        private readonly IHealth _health;
        private readonly IDamagableNotifier _damagableNotifier;

        public InjuryProcessor(IHealth health, IDamagableNotifier damagableNotifier)
        {
            _health = health;
            _damagableNotifier = damagableNotifier;

            _damagableNotifier.Damaged += OnDamaged;
        }

        public void Dispose() => 
            _damagableNotifier.Damaged -= OnDamaged;

        private void OnDamaged(float damage) => 
            _health.TakeDamage(damage);
    }
}