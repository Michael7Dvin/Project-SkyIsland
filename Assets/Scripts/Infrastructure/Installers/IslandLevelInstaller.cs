using Gameplay.Levels;
using Gameplay.Movement.GroundSpherecasting;
using Gameplay.Player;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
using Infrastructure.Services.PlayerDeathService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class IslandLevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerSpawnPoint;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindWorldObjectsLoader();
            BindServices();
        }

        private void BindPlayer()
        {
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IPlayerMovementFactory>().To<PlayerMovementFactory>().AsSingle();
            Container.Bind<IGroundSpherecasterFactory>().To<GroundSpherecasterFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IPlayerDeathService>().To<PlayerDeathService>().AsSingle();
        }

        private void BindWorldObjectsLoader()
        {
            Vector3 playerSpawnPosition = _playerSpawnPoint.position;
            Quaternion playerSpawnRotation = _playerSpawnPoint.rotation;

            Container
                .BindInterfacesTo<IslandWorldObjectsLoader>()
                .AsSingle()
                .WithArguments(playerSpawnPosition, playerSpawnRotation);
        }
    }
}