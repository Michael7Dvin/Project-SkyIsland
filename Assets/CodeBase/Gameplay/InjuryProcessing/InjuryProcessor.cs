using Gameplay.Healths;
using Gameplay.MonoBehaviours.Damagable;

namespace Gameplay.InjuryProcessing
{
    public class InjuryProcessor : IInjuryProcessor
    {
        private readonly IHealth _health;
        private readonly IDamagable _damagable;

        public InjuryProcessor(IHealth health, IDamagable damagable)
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