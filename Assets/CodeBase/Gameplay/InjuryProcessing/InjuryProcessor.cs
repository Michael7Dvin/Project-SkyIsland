using Gameplay.Healths;
using Gameplay.MonoBehaviours;

namespace Gameplay.InjuryProcessing
{
    public class InjuryProcessor
    {
        private readonly IHealth _health;
        private readonly Damagable _damagable;

        public InjuryProcessor(IHealth health, Damagable damagable)
        {
            _health = health;
            _damagable = damagable;

            _damagable.Damaged += OnDamaged;
        }

        public void Dispose() => 
            _damagable.Damaged -= OnDamaged;

        private void OnDamaged(float damage) => 
            _health.TakeDamage(damage);
    }
}