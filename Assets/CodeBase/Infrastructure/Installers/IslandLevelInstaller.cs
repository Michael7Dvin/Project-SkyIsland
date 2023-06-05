using Gameplay.Levels;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Player;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using Gameplay.PlayerDeathService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class IslandLevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerSpawnPoint;
        
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            BindWorldObjectsSpawner();
        }

        private void BindServices()
        {
            Container.Bind<IPlayerDeathService>().To<PlayerDeathService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IPlayerMovementFactory>().To<PlayerMovementFactory>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }

        private void BindWorldObjectsSpawner()
        {
            Container.Bind<IslandWorldData>().AsSingle().WithArguments(_playerSpawnPoint);
            Container.BindInterfacesAndSelfTo<IslandWorldObjectsSpawner>().AsSingle();
        }
    }
}