using Common.Observable;

namespace Gameplay.Healths
{
    public interface IHealth
    {
        float Max { get; }
        IReadOnlyObservable<float> Current { get; }
        
        void TakeDamage(float damage);
        void Heal(float amount);
    }
}