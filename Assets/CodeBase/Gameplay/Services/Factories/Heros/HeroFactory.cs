using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Dying;
using Gameplay.Healths;
using Gameplay.Heros;
using Gameplay.InjuryProcessing;
using Gameplay.MonoBehaviours;
using Gameplay.Movement;
using Gameplay.Services.Factories.Heros.Moving;
using Gameplay.Services.Factories.PlayerCameras;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Factories.HUD;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Factories.Heros
{
    public class HeroFactory : IHeroFactory
    {
        private const string CameraFollowPointName = "Camera Follow Point";
        
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
            GameObject heroGameObject = await CreateHeroGameObject(position, rotation);
            Transform cameraFollowPoint = await CreateCameraFollowPoint(heroGameObject.transform);
            
            GetComponents(heroGameObject,
                out CharacterController characterController,
                out Animator animator,
                out Damagable damageNotifier,
                out Destroyable destroyable);

            IMovement movement = await CreateMovement(heroGameObject.transform,
                characterController,
                animator);

            IHealth health = CreateHealth(_logger);
            await _hudFactory.CreateHealthBar(health);

            InjuryProcessor injuryProcessor = new(health, damageNotifier);
            
            Death death = _instantiator.Instantiate<Death>();
            death.Construct(health, heroGameObject);

            HeroProgressDataProvider heroProgressDataProvider = new(heroGameObject.transform, movement, health);
            
            return new Hero(movement,
                injuryProcessor,
                cameraFollowPoint,
                death,
                destroyable,
                heroProgressDataProvider);
        }

        private async Task<GameObject> CreateHeroGameObject(Vector3 position, Quaternion rotation)
        {
            GameObject prefab = await _commonAssetsProvider.LoadHero();
            GameObject heroGameObject = _instantiator.InstantiatePrefab(prefab, position, rotation, null);
            return heroGameObject;
        }

        private async UniTask<Transform> CreateCameraFollowPoint(Transform hero)
        {
            GameObject prefab = await _commonAssetsProvider.LoadEmptyGameObject();
            GameObject followPoint = _instantiator.InstantiatePrefab(prefab, hero);
            
            followPoint.transform.localPosition = _config.CameraFollowPointOffset;
            followPoint.name = CameraFollowPointName;

            return followPoint.transform;
        }

        private void GetComponents(GameObject hero,
            out CharacterController characterController,
            out Animator animator,
            out Damagable damagable,
            out Destroyable destroyable)
        {
            if (hero.TryGetComponent(out characterController) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(CharacterController)} attached");
            
            if (hero.TryGetComponent(out animator) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(Animator)} attached");
            
            if (hero.TryGetComponent(out damagable) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(Damagable)} attached");
            
            if (hero.TryGetComponent(out destroyable) == false)
                _logger.LogError($"{nameof(hero)} prefab have no {nameof(Destroyable)} attached");
        }

        private async UniTask<IMovement> CreateMovement(Transform parent,
            CharacterController characterController,
            Animator animator)
        {
            return await _movementFactory.Create(parent, animator, characterController);
        }
        
        private IHealth CreateHealth(ICustomLogger logger) =>
            new Health(_config.Stats.CrrentHealth, _config.Stats.MaxHealth, logger);
    }
}