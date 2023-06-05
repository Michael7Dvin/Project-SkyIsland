using System;
using Gameplay.Healths;

namespace Gameplay.Dying
{
    public class Death : IDeath
    {
        private readonly IHealth _health;

        public Death(IHealth health)
        {
            _health = health;

            _health.CurrentValue.Changed += OnHealthChanged;
        }

        private void OnHealthChanged(float health)
        {
            if (health == 0)
                Died?.Invoke();
        }

        public event Action Died;
        
        public void Dispose() => 
            _health.CurrentValue.Changed -= OnHealthChanged;
    }
}