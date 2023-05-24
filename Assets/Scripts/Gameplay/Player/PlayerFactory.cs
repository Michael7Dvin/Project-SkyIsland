using Gameplay.BodyEnvironmentObserving;
using Gameplay.BodyEnvironmentObserving.GroundDetection;
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
        
        private readonly IBodyEnvironmentObserverFactory _bodyEnvironmentObserverFactory;

        public PlayerFactory(IConfigProvider configProvider,
            ICustomLogger logger,
            IPlayerCameraFactory cameraFactory,
            IPlayerMovementFactory movementFactory,
            IBodyEnvironmentObserverFactory bodyEnvironmentObserverFactory)
        {
            _config = configProvider.GetForPlayer();
            _logger = logger;
            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
            _bodyEnvironmentObserverFactory = bodyEnvironmentObserverFactory;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            GameObject playerGameobject = CreatePlayerGameobject(position, rotation);
            GameObject groundDetectorGameobject = CreateGroundDetector(playerGameobject);
            
            _cameraFactory.Create(playerGameobject.transform);

            GetComponents(playerGameobject,
                groundDetectorGameobject,
                out CharacterController characterController,
                out IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier,
                out IGroundDetector groundDetector);
            
            IBodyEnvironmentObserver bodyEnvironmentObserver = _bodyEnvironmentObserverFactory.Create(groundDetector);
            IPlayerMovement movement = _movementFactory.Create(characterController, bodyEnvironmentObserver);
            
            return new Player(movement, bodyEnvironmentObserver, gameObjectLifeCycleNotifier);
        }

        private GameObject CreateGroundDetector(GameObject playerGameobject) => 
            Object.Instantiate(_config.GroundDetectorPrefab, playerGameobject.transform);

        private GameObject CreatePlayerGameobject(Vector3 position, Quaternion rotation) => 
            Object.Instantiate(_config.Prefab, position, rotation);

        private void GetComponents(GameObject player,
            GameObject groundDetectorObject,
            out CharacterController characterController,
            out IGameObjectLifeCycleNotifier gameObjectLifeCycleNotifier,
            out IGroundDetector groundDetector)
        {
            if (player.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(CharacterController)} attached");
            
            if (player.TryGetComponent(out gameObjectLifeCycleNotifier) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(IGameObjectLifeCycleNotifier)} attached");
            
            if (groundDetectorObject.TryGetComponent(out groundDetector) == false)
                _logger.LogError($"{nameof(groundDetectorObject)} prefab have no {nameof(IGroundDetector)} attached");

        }
    }
}