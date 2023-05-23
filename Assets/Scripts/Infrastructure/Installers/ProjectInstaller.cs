using Gameplay.PlayerCamera;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Logger;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.Updater;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScenesInfo _scenesInfo;
        [SerializeField] private PlayerCameraConfig _playerCameraConfig;

        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
            BindGameStateMachine();
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
            
            Container
                .BindInterfacesAndSelfTo<Updater>()
                .AsSingle();
        }
        
        private void BindFactories()
        {
            Container.Bind<PlayerCameraFactory>().AsSingle();
        }
    }
}