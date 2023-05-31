using System;

namespace Gameplay.MonoBehaviours
{
    public interface IDamageNotifier
    {
        public event Action<float> Damaged;

        public void Damage(float damage);
    }
}