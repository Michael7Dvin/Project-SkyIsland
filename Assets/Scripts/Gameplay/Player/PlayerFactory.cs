using Gameplay.BodyEnvironmentObserving;
using Gameplay.BodyEnvironmentObserving.GroundDetection;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using Infrastructure.Services.Configuration;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerConfig _config;
        
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IPlayerMovementFactory _movementFactory;
        
        private readonly IBodyEnvironmentObserverFactory _bodyEnvironmentObserverFactory;

        public PlayerFactory(IConfigProvider configProvider,
            IPlayerCameraFactory cameraFactory,
            IPlayerMovementFactory movementFactory,
            IBodyEnvironmentObserverFactory bodyEnvironmentObserverFactory)
        {
            _config = configProvider.GetForPlayer();
            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
            _bodyEnvironmentObserverFactory = bodyEnvironmentObserverFactory;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            GameObject playerGameobject = 
                CreatePlayerGameobject(position, rotation, out CharacterController characterController);

            _cameraFactory.Create(playerGameobject.transform);

            GroundDetector groundDetector = CreateGroundDetector(playerGameobject);

            IBodyEnvironmentObserver bodyEnvironmentObserver = _bodyEnvironmentObserverFactory.Create(groundDetector);

            PlayerMovement movement = _movementFactory.Create(characterController, bodyEnvironmentObserver);

            Player player = new(movement, bodyEnvironmentObserver);
            return player;
        }

        private GroundDetector CreateGroundDetector(GameObject playerGameobject)
        {
            GameObject groundDetectorGameobject = Object.Instantiate(_config.GroundDetectorPrefab, playerGameobject.transform);
            GroundDetector groundDetector = groundDetectorGameobject.GetComponent<GroundDetector>();
            return groundDetector;
        }

        private GameObject CreatePlayerGameobject(Vector3 position, 
            Quaternion rotation,
            out CharacterController characterController)
        {
            GameObject playerGameobject = Object.Instantiate(_config.Prefab, position, rotation);
            characterController = playerGameobject.GetComponent<CharacterController>();
            return playerGameobject;
        }
    }
}