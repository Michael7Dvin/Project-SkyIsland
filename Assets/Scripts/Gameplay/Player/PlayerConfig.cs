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
        [field: SerializeField] public PlayerMovementConfig Movement { get; private set; }
        [field: SerializeField] public PlayerCameraConfig Camera { get; private set; }
        [field: SerializeField] public PlayerStatsConfig Stats { get; private set; }
    }
}