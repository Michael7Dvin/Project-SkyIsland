using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using Gameplay.PlayerDeathService;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logger;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerConfig _config;
        
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IPlayerMovementFactory _movementFactory;

        private readonly IInstantiator _instantiator;
        private readonly IPlayerDeathService _playerDeathService;
        private readonly ICustomLogger _logger;

        public PlayerFactory(IStaticDataProvider staticDataProvider,
            IPlayerCameraFactory cameraFactory,
            IPlayerMovementFactory movementFactory,
            IInstantiator instantiator,
            IPlayerDeathService playerDeathService,
            ICustomLogger logger)
        {
            _config = staticDataProvider.GetPlayerConfig();

            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
            
            _instantiator = instantiator;
            _playerDeathService = playerDeathService;
            _logger = logger;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            GameObject player = _instantiator.Instantiate(_config.PlayerPrefab.gameObject, position, rotation);
            Camera camera = _cameraFactory.Create(player.transform);

            GetComponents(player,
                out CharacterController characterController,
                out Animator animator,
                out IDamagableNotifier damageNotifier,
                out IGameObjectLifeCycleNotifier playerGameObjectLifeCycleNotifier);

            IPlayerMovement movement = 
                CreatePlayerMovement(player.transform, characterController, animator, camera.transform);

            IHealth health = CreateHealth(_logger);

            IInjuryProcessor injuryProcessor = CreateInjuryProcessor(health, damageNotifier);

            IDeath death = new Death(health);
            _playerDeathService.Initialize(death);
            
            return new Player(player, movement, injuryProcessor, death, playerGameObjectLifeCycleNotifier);
        }

        private void GetComponents(GameObject player,
            out CharacterController characterController,
            out Animator animator,
            out IDamagableNotifier damagableNotifier,
            out IGameObjectLifeCycleNotifier playerGameObjectLifeCycleNotifier)
        {
            if (player.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(CharacterController)} attached");
            
            if (player.TryGetComponent(out animator) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(Animator)} attached");
            
            if (player.TryGetComponent(out damagableNotifier) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(IDamagableNotifier)} attached");
            
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

        private IInjuryProcessor CreateInjuryProcessor(IHealth health, IDamagableNotifier damagableNotifier) =>
            new InjuryProcessor(health, damagableNotifier);

        private IHealth CreateHealth(ICustomLogger logger) =>
            new Health(_config.Stats.CrrentHealth, _config.Stats.MaxHealth, logger);
    }
}