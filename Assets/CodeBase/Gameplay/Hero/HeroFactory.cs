using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Hero.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Gameplay.Hero
{
    public class HeroFactory : IHeroFactory
    {
        private readonly HeroConfig _config;
        
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IHeroMovementFactory _movementFactory;

        private readonly IInstantiator _instantiator;
        private readonly IHeroDeathService _heroDeathService;
        private readonly ICustomLogger _logger;

        public HeroFactory(IStaticDataProvider staticDataProvider,
            IPlayerCameraFactory cameraFactory,
            IHeroMovementFactory movementFactory,
            IInstantiator instantiator,
            IHeroDeathService heroDeathService,
            ICustomLogger logger)
        {
            _config = staticDataProvider.GetPlayerConfig();

            _cameraFactory = cameraFactory;
            _movementFactory = movementFactory;
            
            _instantiator = instantiator;
            _heroDeathService = heroDeathService;
            _logger = logger;
        }

        public Hero Create(Vector3 position, Quaternion rotation)
        {
            GameObject player = _instantiator.Instantiate(_config.HeroPrefab.gameObject, position, rotation);
            Camera camera = _cameraFactory.Create(player.transform);

            GetComponents(player,
                out CharacterController characterController,
                out Animator animator,
                out IDamagableNotifier damageNotifier,
                out IGameObjectLifeCycleNotifier playerGameObjectLifeCycleNotifier);

            IHeroMovement movement = 
                CreatePlayerMovement(player.transform, characterController, animator, camera.transform);

            IHealth health = CreateHealth(_logger);

            IInjuryProcessor injuryProcessor = CreateInjuryProcessor(health, damageNotifier);

            IDeath death = new Death(health);
            _heroDeathService.Init(death);
            
            return new Hero(player, movement, injuryProcessor, death, playerGameObjectLifeCycleNotifier);
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
        
        private IHeroMovement CreatePlayerMovement(Transform parent,
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