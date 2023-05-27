using UnityEngine;

namespace Gameplay.Movement
{
    [CreateAssetMenu(fileName = "Movement Config", menuName = "Configs/Movement")]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
    }
}