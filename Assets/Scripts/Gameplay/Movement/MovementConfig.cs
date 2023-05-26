using UnityEngine;

namespace Gameplay.Movement
{
    [CreateAssetMenu(fileName = "Movement Config", menuName = "Configs/Movement")]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField] private LayerMask _groundLayerMask;

        public LayerMask GroundLayerMask => _groundLayerMask;
    }
}