using Gameplay.Healths;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.Current.Changed += OnHealthChanged;
        }

        private void OnDestroy() => 
            _health.Current.Changed -= OnHealthChanged;

        private void OnHealthChanged(float value) => 
            _fill.fillAmount = _health.Current.Value / _health.Max;
    }
}
