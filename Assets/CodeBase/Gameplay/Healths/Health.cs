using Common.Observable;
using Infrastructure.Services.Logging;
using UnityEngine;

namespace Gameplay.Healths
{
    public class Health : IHealth
    {
        private const float MinValue = 0f;
        private readonly Observable<float> _currentValue = new();

        private readonly ICustomLogger _logger;

        public Health(float initialCurrentValue, float initialMaxValue, ICustomLogger logger)
        {
            ValidateAndSetInitialValues(initialCurrentValue, initialMaxValue);
            _logger = logger;
        }

        public float Max { get; private set; }
        public IReadOnlyObservable<float> Current => _currentValue;

        public void SetCurrent(float newValue) => 
            _currentValue.Value = Mathf.Clamp(newValue, MinValue, Max);

        public void TakeDamage(float damage)
        {
            if (damage <= 0)
            {
                _logger.LogError("Invalid damage amount. Damage must be greater than 0.");
                return;
            }
 
            SetCurrent(_currentValue.Value - damage);
        }

        public void Heal(float amount)
        {
            if (amount <= 0)
            {
                _logger.LogError("Invalid heal amount. Amount must be greater than 0.");
                return;
            }

            SetCurrent(_currentValue.Value + amount);
        }

        private void ValidateAndSetInitialValues(float initialCurrentValue, float initialMaxValue)
        {
            if (initialCurrentValue < 0)
            {
                _logger.LogError("Invalid initial current value. Current value must be non-negative.");
                return;
            }

            if (initialMaxValue < 0)
            {
                _logger.LogError("Invalid initial max value. Max value must be non-negative.");
                return;
            }

            if (initialCurrentValue > initialMaxValue)
            {
                _logger.LogError("Invalid initial values. Current value cannot be greater than the max value.");
                return;
            }

            Max = initialMaxValue;
            _currentValue.Value = initialCurrentValue;
        }
    }
}