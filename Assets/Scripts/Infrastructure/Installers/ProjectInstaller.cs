using Gameplay.Movement;
using Gameplay.Player;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services;
using Infrastructure.Services.Configuration;
using Infrastructure.Services.Input;
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
        
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            BindServices();
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
                .Bind<IConfigProvider>()
                .To<ConfigProvider>()
                .AsSingle()
                .WithArguments(_movementConfig, _playerConfig);
            
            Container
                .BindInterfacesAndSelfTo<Updater>()
                .AsSingle();

            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle();
        }
    }
}