using Cysharp.Threading.Tasks;
using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Hero.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Infrastructure.Services.AssetProviding.Common;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UI.HUD;
using UI.HUD.Factory;
using UnityEngine;

namespace Gameplay.Hero
{
    public class HeroFactory : IHeroFactory
    {
        private readonly HeroConfig _config;

        private readonly IHeroMovementFactory _movementFactory;
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IHUDFactory _hudFactory;
        
        private readonly ICommonAssetsProvider _commonAssetsProvider;
        private readonly IInstantiator _instantiator;
        private readonly IHeroDeathService _heroDeathService;
        private readonly ICustomLogger _logger;

        public HeroFactory(IStaticDataProvider staticDataProvider,
            IHeroMovementFactory movementFactory,
            IPlayerCameraFactory cameraFactory,
            IHUDFactory hudFactory,
            ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator,
            IHeroDeathService heroDeathService,
            ICustomLogger logger)
        {
            _config = staticDataProvider.HeroConfig;

            _movementFactory = movementFactory;
            _cameraFactory = cameraFactory;
            _hudFactory = hudFactory;

            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
            _heroDeathService = heroDeathService;
            _logger = logger;
        }

        public async UniTask WarmUp()
        {
            await _commonAssetsProvider.LoadHero();
            await _movementFactory.WarmUp();
            await _cameraFactory.WarmUp();
            await _hudFactory.WarmUp();
        }

        public async UniTask<Hero> Create(Vector3 position, Quaternion rotation)
        {
            Destroyable heroPrefab = await _commonAssetsProvider.LoadHero();
            
            Destroyable heroDestroyable = _instantiator.Instantiate(heroPrefab, position, rotation);
            GameObject heroGameObject = heroDestroyable.gameObject;
            
            Camera camera = await _cameraFactory.Create(heroDestroyable.transform);
            
            GetComponents(heroGameObject,
                out CharacterController characterController,
                out Animator animator,
                out IDamagableNotifier damageNotifier);

            IHeroMovement movement = await CreatePlayerMovement(heroDestroyable.transform,
                characterController,
                animator,
                camera.transform);

            IHealth health = CreateHealth(_logger);
            await _hudFactory.CreateHealthBar(health);

            IInjuryProcessor injuryProcessor = CreateInjuryProcessor(health, damageNotifier);

            IDeath death = new Death(health);
            _heroDeathService.Init(death);
            
            return new Hero(heroGameObject, movement, injuryProcessor, death, heroDestroyable);
        }

        private void GetComponents(GameObject player,
            out CharacterController characterController,
            out Animator animator,
            out IDamagableNotifier damagableNotifier)
        {
            if (player.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(CharacterController)} attached");
            
            if (player.TryGetComponent(out animator) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(Animator)} attached");
            
            if (player.TryGetComponent(out damagableNotifier) == false)
                _logger.LogError($"{nameof(player)} prefab have no {nameof(IDamagableNotifier)} attached");
        }
        
        private async UniTask<IHeroMovement> CreatePlayerMovement(Transform parent,
            CharacterController characterController,
            Animator animator,
            Transform cameraTransform)
        {
            return await _movementFactory.Create(parent, animator, characterController, cameraTransform);
        }

        private IInjuryProcessor CreateInjuryProcessor(IHealth health, IDamagableNotifier damagableNotifier) =>
            new InjuryProcessor(health, damagableNotifier);

        private IHealth CreateHealth(ICustomLogger logger) =>
            new Health(_config.Stats.CrrentHealth, _config.Stats.MaxHealth, logger);
    }
}