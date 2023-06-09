using Gameplay.Hero;
using Gameplay.Levels;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services.AppClosing;
using Infrastructure.Services.Input;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updater;
using UI;
using UI.Services.Factory;
using UI.Services.WindowsOperating;
using UnityEngine;
using Zenject;
using IInstantiator = Infrastructure.Services.Instantiating.IInstantiator;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private HeroConfig _heroConfig;
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
            Container.Bind<InitializationState>().AsSingle();
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
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
            Container.Bind<IWindowsService>().To<WindowsService>().AsSingle();
            Container.Bind<IAppCloser>().To<AppCloser>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Updater>().AsSingle();
        }

        private void BindProvidingServices()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_heroConfig, _uiConfig, _scenesData);
        }
    }
}