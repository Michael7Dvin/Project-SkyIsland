using Gameplay.Services.Creation.GroundSpherecasting;
using Gameplay.Services.Creation.HeroMoving;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.HeroProviding;
using Gameplay.Services.PlayerPausing;
using Infrastructure.LevelLoading.Data;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Progress.Services;
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
            BindFactories();
            BindLevelLoadingServices();
        }

        private void BindServices()
        {
            Container.Bind<IHeroDeathService>().To<HeroDeathService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPause>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
            Container.Bind<IIslandWorldProgressHandler>().To<IslandWorldProgressHandler>().AsSingle();
            Container.Bind<IHeroProgressHandler>().To<HeroProgressHandler>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IHeroMovementFactory>().To<HeroMovementFactory>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
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