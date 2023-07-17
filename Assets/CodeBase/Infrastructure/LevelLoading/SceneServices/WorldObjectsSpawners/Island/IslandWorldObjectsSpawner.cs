using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.PlayerCameras;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.Providers.HeroProviding;
using Gameplay.Services.Providers.PlayerCameraProviding;
using Gameplay.Services.Spawners.Heros;
using Gameplay.Services.Spawners.PlayerCameras;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.LevelLoading.WorldData;
using Infrastructure.Services.Input;
using UI.Services.Providing.Utilities;
using UI.Services.Spawners;
using UnityEngine;

namespace Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners.Island
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly IslandWorldData _worldData;

        private readonly IUiUtilitiesSpawner _uiUtilitiesSpawner;
        private readonly IHeroSpawner _heroSpawner;
        private readonly IPlayerCameraSpawner _playerCameraSpawner;

        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;
        private readonly IHeroProvider _heroProvider;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        
        private readonly IHeroDeathService _heroDeathService;
        private readonly IInputService _inputService;

        public IslandWorldObjectsSpawner(ISceneServicesProvider sceneServicesProvider,
            IslandWorldData worldData,
            IUiUtilitiesSpawner uiUtilitiesSpawner,
            IHeroSpawner heroSpawner,
            IPlayerCameraSpawner playerCameraSpawner,
            IUiUtilitiesProvider uiUtilitiesProvider,
            IHeroProvider heroProvider,
            IPlayerCameraProvider playerCameraProvider,
            IHeroDeathService heroDeathService,
            IInputService inputService)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _worldData = worldData;
            _uiUtilitiesSpawner = uiUtilitiesSpawner;
            _heroSpawner = heroSpawner;
            _playerCameraSpawner = playerCameraSpawner;
            _uiUtilitiesProvider = uiUtilitiesProvider;
            _heroProvider = heroProvider;
            _playerCameraProvider = playerCameraProvider;
            _heroDeathService = heroDeathService;
            _inputService = inputService;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            await SpawnUIUtilities();
            Hero hero = await SpawnHero();
            await SpawnPlayerCamera(hero.CameraFollowPoint);
        }

        private async UniTask SpawnUIUtilities()
        {
            if (_uiUtilitiesProvider.Canvas.Value == null) 
                await _uiUtilitiesSpawner.SpawnCanvas();

            if (_uiUtilitiesProvider.EventSystem.Value == null) 
                await _uiUtilitiesSpawner.SpawnEventSystem();
        }
        
        private async UniTask<Hero> SpawnHero()
        {
            Hero hero = _heroProvider.Hero.Value;
            
            if (hero == null)
            {
                Vector3 position = _worldData.HeroSpawnPoint.position;
                Quaternion rotation = _worldData.HeroSpawnPoint.rotation;
                hero = await _heroSpawner.Spawn(position, rotation);
            }
            
            _heroDeathService.Reinitialize(hero.Death);
            return hero;
        }

        private async UniTask SpawnPlayerCamera(Transform heroFollowPoint)
        {
            PlayerCamera playerCamera = _playerCameraProvider.PlayerCamera.Value;
            
            if (playerCamera == null) 
                playerCamera = await _playerCameraSpawner.Spawn();

            playerCamera.PlayerCameraController.SetFollowPoint(heroFollowPoint);
            _inputService.Hero.SetHorizontalDirectionAligningCamera(playerCamera);
        }
        
        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetWorldObjectsSpawner(this);
    }
}