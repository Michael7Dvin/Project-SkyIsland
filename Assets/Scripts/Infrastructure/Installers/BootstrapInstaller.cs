using Infrastructure.Configuration;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.SceneLoading;
using Infrastructure.Services;
using Infrastructure.Services.Logger;
using PlayerCamera;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ScenesInfo _scenesInfo;
        [SerializeField] private PlayerCameraConfig _playerCameraConfig;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
            BindFactories();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LoadSceneState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<SceneLoader>().AsSingle();
            
            Container
                .Bind<ICustomLogger>()
                .To<CustomLogger>()
                .AsSingle();
            
            Container
                .Bind<UtilityDataProvider>()
                .AsSingle()
                .WithArguments(_scenesInfo);

            Container
                .Bind<ConfigProvider>()
                .AsSingle()
                .WithArguments(_playerCameraConfig);
        }

        private void BindFactories()
        {
            Container.Bind<PlayerCameraFactory>().AsSingle();
        }
    }
}