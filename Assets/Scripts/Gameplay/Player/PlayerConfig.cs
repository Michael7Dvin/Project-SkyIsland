using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public GameObject GroundDetectorPrefab { get; private set; }

        [field: SerializeField] public PlayerMovementConfig Movement { get; private set; }
        [field: SerializeField] public PlayerCameraConfig Camera { get; private set; }
    }
}