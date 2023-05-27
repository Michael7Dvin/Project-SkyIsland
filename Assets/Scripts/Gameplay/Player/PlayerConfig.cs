using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObjectLifeCycleNotifier _playerPrefab;
        [SerializeField] private CollisionsNotifier _groundTypeObserverPrefab;
        [SerializeField] private PlayerMovementConfig _movement;
        [SerializeField] private PlayerCameraConfig _camera;
        
        public GameObjectLifeCycleNotifier PlayerPrefab => _playerPrefab;
        public CollisionsNotifier GroundTypeObserverPrefab => _groundTypeObserverPrefab;
        public PlayerMovementConfig Movement => _movement;
        public PlayerCameraConfig Camera => _camera;
    }
}