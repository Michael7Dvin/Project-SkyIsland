using Gameplay.Hero;
using Gameplay.Hero.Movement;
using Gameplay.Hero.PlayerCamera;
using Gameplay.HeroDeathService;
using Gameplay.Levels;
using Gameplay.Movement.GroundSpherecasting;
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
            BindWorldObjectsSpawner();
        }

        private void BindServices()
        {
            Container.Bind<IHeroDeathService>().To<HeroDeathService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IHeroCameraFactory>().To<HeroCameraFactory>().AsSingle();
            Container.Bind<IHeroMovementFactory>().To<HeroMovementFactory>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }

        private void BindWorldObjectsSpawner()
        {
            Container.Bind<IslandWorldData>().AsSingle().WithArguments(_heroSpawnPoint);
            Container.BindInterfacesAndSelfTo<IslandWorldObjectsSpawner>().AsSingle();
        }
    }
}