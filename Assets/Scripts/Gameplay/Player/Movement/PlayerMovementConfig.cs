using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float JogSpeed { get; private set; }
        [field: SerializeField] public float AntiBumpSpeed { get; private set; }

        [field: SerializeField] public float FallVerticalSpeed { get; private set; }
        [field: SerializeField] public float FallHorizontalSpeed { get; private set; }

        [field: SerializeField] public GameObject GroundSphereCastingPointPrefab { get; private set; }
        [field: SerializeField] public float GroundSphereCastingSphereRadius { get; private set; }
        [field: SerializeField] public float GroundSphereCastingDistance { get; private set; }
    }
}