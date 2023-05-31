using Common.Observable;

namespace Gameplay.Healths
{
    public interface IHealth
    {
        public IReadOnlyObservable<float> CurrentValue { get; }

        public void TakeDamage(float damage);
        public void Heal(float amount);
    }
}