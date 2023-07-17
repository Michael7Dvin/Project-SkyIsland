using Gameplay.Services.Factories.GroundSpherecasting;
using Gameplay.Services.Factories.Heros;
using Gameplay.Services.Factories.Heros.Moving;
using Gameplay.Services.Factories.PlayerCameras;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.PlayerPausing;
using Gameplay.Services.Providers.HeroProviding;
using Gameplay.Services.Providers.PlayerCameraProviding;
using Gameplay.Services.Spawners.Heros;
using Gameplay.Services.Spawners.PlayerCameras;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.LevelLoading.SceneServices.WarmUppers.Island;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners.Island;
using Infrastructure.LevelLoading.WorldData;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.Instantiating;
using UnityEngine;
using Zenject;
using IInstantiator = Infrastructure.Services.Instantiating.IInstantiator;

namespace Infrastructure.Installers.Levels
{
    public class IslandLevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _heroSpawnPoint;
        
        public override void InstallBindings()
        {
            BindServices();
            BindSpawners();
            BindFactories();
            BindProviders();
            BindProgressHandlers();
            BindLevelLoadingServices();
        }

        private void BindServices()
        {
            Container.Bind<IHeroDeathService>().To<HeroDeathService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPause>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<IHeroSpawner>().To<HeroSpawner>().AsSingle();
            Container.Bind<IPlayerCameraSpawner>().To<PlayerCameraSpawner>().AsSingle();
        }
        
        private void BindFactories()
        {
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IHeroMovementFactory>().To<HeroMovementFactory>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
        }

        private void BindProviders()
        {
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
            Container.Bind<IPlayerCameraProvider>().To<PlayerCameraProvider>().AsSingle();
        }

        private void BindProgressHandlers()
        {
            Container.Bind<IIslandWorldProgressHandler>().To<IslandWorldProgressHandler>().AsSingle();
            Container.Bind<IHeroProgressHandler>().To<HeroProgressHandler>().AsSingle();
        }

        private void BindLevelLoadingServices()
        {
            Container.Bind<IslandWorldData>().AsSingle().WithArguments(_heroSpawnPoint);
            Container.BindInterfacesAndSelfTo<IslandWarmUpper>().AsSingle();
            Container.BindInterfacesAndSelfTo<IslandWorldObjectsSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<IslandLevelProgressService>().AsSingle();
        }
    }
}