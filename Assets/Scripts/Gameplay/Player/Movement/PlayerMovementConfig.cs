using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _fallSpeed;
        
        public float FallSpeed => _fallSpeed;
    }
}