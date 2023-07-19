using Gameplay.Services.Factories.Heros;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.SceneServices.SceneServicesProviding;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.AssetProviding.Providers.ForCamera;
using Infrastructure.Services.Destroying;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.Pausing;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updating;
using UI.Services.Factories.Window;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllAssetsAddresses _allAssetsAddresses;
        [SerializeField] private AllScenesData _allScenesData;
        
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private WindowsConfigs _windowsConfigs;

        public override void InstallBindings()
        {
            BindServices();
            BindProviders();
            BindGameStateMachine();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LevelLoadingState>().AsSingle();
            Container.Bind<LevelRestartState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<QuitState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IDestroyer>().To<Destroyer>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Updater>().AsSingle();
        }

        private void BindProviders()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_allAssetsAddresses, _allScenesData, _heroConfig, _windowsConfigs);
            
            Container.Bind<ISceneServicesProvider>().To<SceneServicesProvider>().AsSingle();

            Container.Bind<ICommonAssetsProvider>().To<CommonAssetsProvider>().AsSingle();
            Container.Bind<ICameraAssetsProvider>().To<CameraAssetsProvider>().AsSingle();
        }
    }
}