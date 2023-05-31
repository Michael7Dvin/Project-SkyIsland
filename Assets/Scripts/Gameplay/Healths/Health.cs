using Common.Observable;
using Infrastructure.Services.Logger;
using UnityEngine;

namespace Gameplay.Healths
{
    public class Health : IHealth
    {
        private const float MinValue = 0f;
        private float _maxValue;
        private readonly Observable<float> _currentValue = new();

        private readonly ICustomLogger _logger;

        public Health(float initialCurrentValue, float initialMaxValue, ICustomLogger logger)
        {
            ValidateAndSetInitialValues(initialCurrentValue, initialMaxValue);
            _logger = logger;
        }

        public IReadOnlyObservable<float> CurrentValue => _currentValue;

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

        private void SetCurrent(float newValue) => 
            _currentValue.Value = Mathf.Clamp(newValue, MinValue, _maxValue);

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

            _maxValue = initialMaxValue;
            _currentValue.Value = initialCurrentValue;
        }
    }
}