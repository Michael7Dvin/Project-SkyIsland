using Cysharp.Threading.Tasks;
using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Heros;
using Gameplay.Heros.Movement;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours.Damagable;
using Gameplay.MonoBehaviours.Destroyable;
using Gameplay.Services.Creation.HeroMoving;
using Gameplay.Services.Creation.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.Destroying;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Factories.HUD;
using UnityEngine;

namespace Gameplay.Services.Creation.Heros.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly HeroConfig _config;

        private readonly IHeroMovementFactory _movementFactory;
        private readonly IPlayerCameraFactory _cameraFactory;
        private readonly IHUDFactory _hudFactory;
        
        private readonly ICommonAssetsProvider _commonAssetsProvider;
        private readonly IInstantiator _instantiator;
        private readonly ICustomLogger _logger;

        public HeroFactory(IStaticDataProvider staticDataProvider,
            IHeroMovementFactory movementFactory,
            IPlayerCameraFactory cameraFactory,
            IHUDFactory hudFactory,
            ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator,
            ICustomLogger logger)
        {
            _config = staticDataProvider.HeroConfig;

            _movementFactory = movementFactory;
            _cameraFactory = cameraFactory;
            _hudFactory = hudFactory;

            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
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
            GameObject prefab = await _commonAssetsProvider.LoadHero();
            GameObject heroGameObject = _instantiator.InstantiatePrefab(prefab, position, rotation);
            
            Camera camera = await _cameraFactory.Create(heroGameObject.transform);
            
            GetComponents(heroGameObject,
                out CharacterController characterController,
                out Animator animator,
                out IDamagable damageNotifier,
                out IDestroyable destroyable);

            IMovement movement = await CreateMovement(heroGameObject.transform,
                characterController,
                animator,
                camera.transform);

            IHealth health = CreateHealth(_logger);
            await _hudFactory.CreateHealthBar(health);

            IInjuryProcessor injuryProcessor = CreateInjuryProcessor(health, damageNotifier);
            
            Death death = _instantiator.Instantiate<Death>();
            death.Construct(health, heroGameObject);

            IHeroProgressDataProvider heroProgressDataProvider =
                new HeroProgressDataProvider(heroGameObject.transform, movement, health);
            
            return new Hero(heroGameObject,
                movement,
                health,
                injuryProcessor,
                death,
                destroyable,
                heroProgressDataProvider);
        }

        private void GetComponents(GameObject hero,
            out CharacterController characterController,
            out Animator animator,
            out IDamagable damagable,
            out IDestroyable destroyable)
        {
            if (hero.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(CharacterController)} attached");
            
            if (hero.TryGetComponent(out animator) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(Animator)} attached");
            
            if (hero.TryGetComponent(out damagable) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(IDamagable)} attached");
            
            if (hero.TryGetComponent(out destroyable) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(IDestroyable)} attached");
        }
        
        private async UniTask<IMovement> CreateMovement(Transform parent,
            CharacterController characterController,
            Animator animator,
            Transform cameraTransform)
        {
            return await _movementFactory.Create(parent, animator, characterController, cameraTransform);
        }

        private IInjuryProcessor CreateInjuryProcessor(IHealth health, IDamagable damagable) =>
            new InjuryProcessor(health, damagable);

        private IHealth CreateHealth(ICustomLogger logger) =>
            new Health(_config.Stats.CrrentHealth, _config.Stats.MaxHealth, logger);
    }
}