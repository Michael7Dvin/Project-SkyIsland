using Gameplay.Services.Creation.GroundSpherecasting;
using Gameplay.Services.Creation.HeroMoving;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.PlayerPausing;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Progress.LevelProgressLoading;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.WarmUpping;
using Infrastructure.Services.LevelLoading.WorldObjectsSpawning;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
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
            Container.Bind<HeroProgressHandler>().AsSingle();
            Container.Bind<IslandLevelProgressHandler>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<IslandProgressLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<IslandWarmUpper>().AsSingle();
            Container.BindInterfacesAndSelfTo<IslandWorldObjectsSpawner>().AsSingle();
        }
    }
}