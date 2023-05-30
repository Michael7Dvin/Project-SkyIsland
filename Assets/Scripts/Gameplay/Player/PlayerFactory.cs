using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Logger;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerConfig _config;

        private readonly ICustomLogger _logger;
        
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IPlayerMovementFactory _movementFactory;

        public PlayerFactory(IConfigProvider configProvider,
            ICustomLogger logger,
            IPlayerCameraFactory cameraFactory,
            IPlayerMovementFactory movementFactory)
        {
            _config = configProvider.GetForPlayer();
            _logger = logger;
            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            GameObject player = Object.Instantiate(_config.PlayerPrefab.gameObject, position, rotation);
            Camera camera = _cameraFactory.Create(player.transform);

            GetComponents(player,
                out CharacterController characterController,
                out Animator animator,
                out IGameObjectLifeCycleNotifier playerGameObjectLifeCycleNotifier);

            IPlayerMovement movement = 
                CreatePlayerMovement(player.transform, characterController, animator, camera.transform);
            
            return new Player(movement, playerGameObjectLifeCycleNotifier);
        }

        private void GetComponents(GameObject player,
            out CharacterController characterController,
            out Animator animator,
            out IGameObjectLifeCycleNotifier playerGameObjectLifeCycleNotifier)
        {
            if (player.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(CharacterController)} attached");
            
            if (player.TryGetComponent(out animator) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(Animator)} attached");
            
            if (player.TryGetComponent(out playerGameObjectLifeCycleNotifier) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(IGameObjectLifeCycleNotifier)} attached");
        }
        
        private IPlayerMovement CreatePlayerMovement(Transform parent,
            CharacterController characterController,
            Animator animator,
            Transform cameraTransform)
        {
            return _movementFactory.Create(parent, animator, characterController, cameraTransform);
        }
    }
}