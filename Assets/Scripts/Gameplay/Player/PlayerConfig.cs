using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObjectLifeCycleObserver _playerPrefab;
        [SerializeField] private CollisionObserver _groundTypeObserverPrefab;
        [SerializeField] private PlayerMovementConfig _movement;
        [SerializeField] private PlayerCameraConfig _camera;
        
        public GameObjectLifeCycleObserver PlayerPrefab => _playerPrefab;
        public CollisionObserver GroundTypeObserverPrefab => _groundTypeObserverPrefab;
        public PlayerMovementConfig Movement => _movement;
        public PlayerCameraConfig Camera => _camera;
    }
}