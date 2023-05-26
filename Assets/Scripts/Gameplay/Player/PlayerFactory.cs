using Gameplay.GroundTypeObserving;
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
        private readonly IGroundTypeObserverFactory _groundTypeObserverFactory;

        public PlayerFactory(IConfigProvider configProvider,
            ICustomLogger logger,
            IPlayerCameraFactory cameraFactory,
            IPlayerMovementFactory movementFactory,
            IGroundTypeObserverFactory groundTypeObserverFactory)
        {
            _config = configProvider.GetForPlayer();
            _logger = logger;
            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
            _groundTypeObserverFactory = groundTypeObserverFactory;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            GameObject player = Object.Instantiate(_config.PlayerPrefab.gameObject, position, rotation);
            Camera camera = _cameraFactory.Create(player.transform);

            GetComponents(player,
                out CharacterController characterController,
                out IGameObjectLifeCycleObserver playerGameObjectLifeCycleNotifier);

            IGroundTypeObserver groundTypeObserver = CreateGroundTypeObserver(player);
            
            IPlayerMovement movement = CreatePlayerMovement(player.transform,
                characterController,
                groundTypeObserver,
                camera.transform);
            
            return new Player(movement, playerGameObjectLifeCycleNotifier);
        }

        private void GetComponents(GameObject player,
            out CharacterController characterController,
            out IGameObjectLifeCycleObserver playerGameObjectLifeCycleObserver)
        {
            if (player.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(CharacterController)} attached");
            
            if (player.TryGetComponent(out playerGameObjectLifeCycleObserver) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(IGameObjectLifeCycleObserver)} attached");
        }

        private IGroundTypeObserver CreateGroundTypeObserver(GameObject player) => 
            _groundTypeObserverFactory.Create(player.transform, _config.GroundTypeObserverPrefab);

        private IPlayerMovement CreatePlayerMovement(Transform parent, CharacterController characterController,
            IGroundTypeObserver groundTypeObserver,
            Transform cameraTransform)
        {
            return _movementFactory.Create(parent, characterController, groundTypeObserver, cameraTransform);
        }
    }
}