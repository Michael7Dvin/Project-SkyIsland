using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Configs/Player/Stats")]
    public class PlayerStatsConfig : ScriptableObject
    {
        [Header("Health")] 
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public float CrrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
    }
}