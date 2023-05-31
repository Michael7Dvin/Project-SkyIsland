using Common.Observable;

namespace Gameplay.Health
{
    public interface IHealth
    {
        public IReadOnlyObservable<float> CurrentValue { get; }

        public void AcceptDamage(float damage);
        public void Heal(float amount);
    }
}