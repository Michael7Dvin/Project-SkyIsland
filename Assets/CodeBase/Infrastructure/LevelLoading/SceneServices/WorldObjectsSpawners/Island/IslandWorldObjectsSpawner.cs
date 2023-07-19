using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.PlayerCameras;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.Providers.HeroProviding;
using Gameplay.Services.Providers.PlayerCameraProviding;
using Gameplay.Services.Spawners.Heros;
using Gameplay.Services.Spawners.PlayerCameras;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.Services.Input;
using UI.Services.Providing.Utilities;
using UI.Services.Spawners;
using UnityEngine;

namespace Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners.Island
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly Transform _heroSpawnPoint;
        
        private readonly ISceneServicesProvider _sceneServicesProvider;

        private readonly IUiUtilitiesSpawner _uiUtilitiesSpawner;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;
        
        private readonly IHeroSpawner _heroSpawner;
        private readonly IHeroProvider _heroProvider;
        
        private readonly IPlayerCameraSpawner _playerCameraSpawner;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        
        private readonly IHeroDeathService _heroDeathService;
        private readonly IInputService _inputService;

        public IslandWorldObjectsSpawner(Transform heroSpawnPoint,
            ISceneServicesProvider sceneServicesProvider,
            IUiUtilitiesSpawner uiUtilitiesSpawner,
            IUiUtilitiesProvider uiUtilitiesProvider,
            IHeroSpawner heroSpawner,
            IHeroProvider heroProvider,
            IPlayerCameraSpawner playerCameraSpawner,
            IPlayerCameraProvider playerCameraProvider,
            IHeroDeathService heroDeathService,
            IInputService inputService)
        {
            _heroSpawnPoint = heroSpawnPoint;
            
            _sceneServicesProvider = sceneServicesProvider;
            
            _uiUtilitiesSpawner = uiUtilitiesSpawner;
            _uiUtilitiesProvider = uiUtilitiesProvider;
            
            _heroSpawner = heroSpawner;
            _heroProvider = heroProvider;
            
            _playerCameraSpawner = playerCameraSpawner;
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

        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetWorldObjectsSpawner(this);

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
                Vector3 position = _heroSpawnPoint.position;
                Quaternion rotation = _heroSpawnPoint.rotation;
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
    }
}