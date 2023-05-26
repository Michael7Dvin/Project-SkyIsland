using Gameplay.GroundTypeObserving;
using Gameplay.Levels;
using Gameplay.Player;
using Gameplay.Player.Movement;
using Gameplay.Player.PlayerCamera;
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
            BindLevelSetuper();
        }

        private void BindPlayer()
        {
            Container.Bind<IPlayerCameraFactory>().To<PlayerCameraFactory>().AsSingle();
            Container.Bind<IPlayerMovementFactory>().To<PlayerMovementFactory>().AsSingle();
            Container.Bind<IGroundTypeObserverFactory>().To<GroundTypeObserverFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }

        private void BindLevelSetuper()
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