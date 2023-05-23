using UnityEngine;

namespace Gameplay.Player.Movement
{
    
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float FallSpeed;
    }
}