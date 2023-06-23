using UnityEngine;

namespace Gameplay.Services.Factories.HeroFactory
{
    [CreateAssetMenu(fileName = "Hero Stats", menuName = "Configs/Hero/Stats")]
    public class HeroStatsConfig : ScriptableObject
    {
        [Header("Health")] 
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public float CrrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
    }
}