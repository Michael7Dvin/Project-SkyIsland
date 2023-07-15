using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCamera;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading.LevelServicesProviding;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.AssetProviding.Providers.ForCamera;
using Infrastructure.Services.AssetProviding.Providers.UI.All;
using Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds;
using Infrastructure.Services.AssetProviding.Providers.UI.HUD;
using Infrastructure.Services.AssetProviding.Providers.UI.Windows;
using Infrastructure.Services.Destroying;
using Infrastructure.Services.Input;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.Pausing;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using Infrastructure.Services.Updating;
using UI.Services.Factories.Background;
using UI.Services.Factories.HUD;
using UI.Services.Factories.UI;
using UI.Services.Factories.Window;
using UI.Services.Operating;
using UnityEngine;
using Zenject;
using IInstantiator = Infrastructure.Services.Instantiating.IInstantiator;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AllAssetsAddresses _allAssetsAddresses;
        [SerializeField] private ScenesData _scenesData;
        
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private PlayerCameraConfig _playerCameraConfig;
        [SerializeField] private WindowsConfigs _windowsConfigs;

        public override void InstallBindings()
        {
            BindServices();
            BindProviders();
            BindGameStateMachine();
            BindUI();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.Bind<LevelLoadingState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<QuitState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ICustomLogger>().To<CustomLogger>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IPauseService>().To<PauseService>().AsCached();
            Container.Bind<IInstantiator>().To<Instantiator>().AsCached();
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
                .WithArguments(_allAssetsAddresses, _scenesData, _heroConfig, _windowsConfigs, _playerCameraConfig);
            
            Container.Bind<ILevelServicesProvider>().To<LevelServicesProvider>().AsSingle();

            Container.Bind<ICommonAssetsProvider>().To<CommonAssetsProvider>().AsSingle();
            Container.Bind<ICameraAssetsProvider>().To<CameraAssetsProvider>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<IWindowsOperator>().To<WindowsOperator>().AsSingle();
            
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
            Container.Bind<IBackgroundFactory>().To<BackgroundFactory>().AsSingle();
            Container.Bind<IHUDFactory>().To<HUDFactory>().AsSingle();
            
            Container.Bind<IUIAssetsProvider>().To<UIAssetsProvider>().AsSingle();
            Container.Bind<IWindowsAssetsProvider>().To<WindowsAssetsProvider>().AsSingle();
            Container.Bind<IBackgroundsAssetsProvider>().To<BackgroundsAssetsProvider>().AsSingle();
            Container.Bind<IHUDAssetsProvider>().To<HUDAssetsProvider>().AsSingle();
        }
    }
}