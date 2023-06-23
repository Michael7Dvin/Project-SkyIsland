using System;

namespace Gameplay.MonoBehaviours.Damagable
{
    public interface IDamagable
    {
        public event Action<float> Damaged;

        public void TakeDamage(float damage);
    }
}