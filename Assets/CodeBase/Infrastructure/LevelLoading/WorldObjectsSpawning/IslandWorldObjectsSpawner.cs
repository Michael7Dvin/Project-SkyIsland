using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.PlayerCameras;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCameras;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.Providing.HeroProviding;
using Gameplay.Services.Providing.PlayerCameraProviding;
using Infrastructure.LevelLoading.Data;
using Infrastructure.LevelLoading.LevelServicesProviding;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Infrastructure.LevelLoading.WorldObjectsSpawning
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IslandWorldData _worldData;
        
        private readonly IHeroFactory _heroFactory;
        private readonly IPlayerCameraFactory _playerCameraFactory;
        
        private readonly IHeroProvider _heroProvider;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        
        private readonly IHeroDeathService _heroDeathService;
        private readonly IInputService _inputService;

        public IslandWorldObjectsSpawner(ILevelServicesProvider levelServicesProvider,
            IslandWorldData worldData,
            IHeroFactory heroFactory,
            IPlayerCameraFactory playerCameraFactory,
            IHeroProvider heroProvider,
            IPlayerCameraProvider playerCameraProvider,
            IHeroDeathService heroDeathService,
            IInputService inputService)
        {
            _levelServicesProvider = levelServicesProvider;
            _worldData = worldData;
            
            _heroFactory = heroFactory;
            _playerCameraFactory = playerCameraFactory;
            
            _heroProvider = heroProvider;
            _playerCameraProvider = playerCameraProvider;
            
            _heroDeathService = heroDeathService;
            _inputService = inputService;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            Hero hero = await SpawnHero();
            await SpawnPlayerCamera(hero.GameObject.transform);
        }

        private async UniTask<Hero> SpawnHero()
        {
            Hero hero = _heroProvider.Hero.Value;
            
            if (hero == null)
            {
                Vector3 position = _worldData.HeroSpawnPoint.position;
                Quaternion rotation = _worldData.HeroSpawnPoint.rotation;
                hero = await _heroFactory.Create(position, rotation);
                
                _heroProvider.Set(hero);
                _heroDeathService.Init(hero.Death);
            }

            return hero;
        }

        private async UniTask SpawnPlayerCamera(Transform hero)
        {
            if (_playerCameraProvider.PlayerCamera.Value == null)
            {
                PlayerCamera playerCamera = await _playerCameraFactory.Create(hero);

                _playerCameraProvider.Set(playerCamera);
                _inputService.Hero.SetHorizontalDirectionAligningCamera(playerCamera);
            }
        }
        
        private void SetSelfToProvider() => 
            _levelServicesProvider.SetWorldObjectsSpawner(this);
    }
}