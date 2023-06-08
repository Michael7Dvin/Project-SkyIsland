using Gameplay.Levels;
using Gameplay.Player;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services.AppClosing;
using Infrastructure.Services.Input;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updater;
using UI;
using UI.Services.Factory;
using UnityEngine;
using Zenject;
using IInstantiator = Infrastructure.Services.Instantiating.IInstantiator;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private UIConfig _uiConfig;
        [SerializeField] private ScenesData _scenesData;
        
        public override void InstallBindings()
        {
            BindServices();
            BindProvidingServices();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IInstantiator>().To<Instantiator>().AsSingle();
            Container.Bind<IWorldObjectsSpawnerProvider>().To<WorldObjectsSpawnerProvider>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IAppCloser>().To<AppCloser>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Updater>().AsSingle();
        }

        private void BindProvidingServices()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_playerConfig, _uiConfig, _scenesData);
        }
    }
}