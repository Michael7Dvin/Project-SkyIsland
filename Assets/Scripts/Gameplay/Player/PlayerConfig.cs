using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public GameObjectLifeCycleNotifier PlayerPrefab { get; private set; }
        [field: SerializeField] public PlayerMovementConfig MovementConfig { get; private set; }
        [field: SerializeField] public PlayerCameraConfig CameraConfig { get; private set; }
    }
}