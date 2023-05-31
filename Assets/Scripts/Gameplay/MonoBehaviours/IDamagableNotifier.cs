using System;

namespace Gameplay.MonoBehaviours
{
    public interface IDamagableNotifier
    {
        public event Action<float> Damaged;

        public void Damage(float damage);
    }
}