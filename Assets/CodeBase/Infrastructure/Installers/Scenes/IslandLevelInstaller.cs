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
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.SceneServicesSetting;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Scenes
{
    public class IslandLevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _heroSpawnPoint;
        
        public override void InstallBindings()
        {
            BindHero();
            BindPlayerCamera();
            BindSceneServices();
            BindServices();
        }

        private void BindHero()
        {
            Container.Bind<IHeroDeathService>().To<HeroDeathService>().AsSingle();
            Container.Bind<IHeroProgressHandler>().To<HeroProgressHandler>().AsSingle();

            Container.Bind<IHeroSpawner>().To<HeroSpawner>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
            Container.Bind<IHeroMovementFactory>().To<HeroMovementFactory>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        }

        private void BindPlayerCamera()
        {
            Container.Bind<IPlayerCameraSpawner>().To<PlayerCameraSpawner>().AsSingle();
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IPlayerCameraProvider>().To<PlayerCameraProvider>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<PlayerPause>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
            Container.Bind<IIslandWorldProgressHandler>().To<IslandWorldProgressHandler>().AsSingle();
        }

        private void BindSceneServices()
        {
            Container.BindInterfacesTo<IslandServicesSetter>().AsSingle();
            
            Container.Bind<IWarmUpper>().To<IslandWarmUpper>().AsSingle();
            Container.Bind<IWorldObjectsSpawner>().To<IslandWorldObjectsSpawner>().AsSingle().WithArguments(_heroSpawnPoint);
            Container.Bind<ILevelProgressService>().To<IslandLevelProgressService>().AsSingle();
        }
    }
}