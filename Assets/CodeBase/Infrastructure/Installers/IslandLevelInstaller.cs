using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Services.Factories.GroundSpherecaster;
using Gameplay.Services.Factories.HeroFactory;
using Gameplay.Services.Factories.HeroMovement;
using Gameplay.Services.Factories.PlayerCamera;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.PlayerPausing;
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
            Container.BindInterfacesAndSelfTo<IslandWorldObjectsSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<IslandWarmUpper>().AsSingle();
        }
    }
}