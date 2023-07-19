using Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds;
using Infrastructure.Services.AssetProviding.Providers.UI.HUD;
using Infrastructure.Services.AssetProviding.Providers.UI.Utilities;
using Infrastructure.Services.AssetProviding.Providers.UI.Windows;
using UI.Services.Factories.Background;
using UI.Services.Factories.HUD;
using UI.Services.Factories.Utilities;
using UI.Services.Factories.Window;
using UI.Services.Operating;
using UI.Services.Providing.Utilities;
using UI.Services.Spawners;
using Zenject;

namespace Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUiUtilities();
            BindWindows();
            BindBackGrounds();
            BindHUD();
        }

        private void BindUiUtilities()
        {
            Container.Bind<IUiUtilitiesSpawner>().To<UiUtilitiesSpawner>().AsSingle();
            Container.Bind<IUiUtilitiesFactory>().To<UiUtilitiesFactory>().AsSingle();
            Container.Bind<IUiUtilitiesProvider>().To<UiUtilitiesProvider>().AsSingle();
            Container.Bind<IUiUtilitiesAssetsProvider>().To<UiUtilitiesAssetsProvider>().AsSingle();
        }

        private void BindWindows()
        {
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
            Container.Bind<IWindowsAssetsProvider>().To<WindowsAssetsProvider>().AsSingle();
            Container.Bind<IWindowsOperator>().To<WindowsOperator>().AsSingle();
        }

        private void BindBackGrounds()
        {
            Container.Bind<IBackgroundFactory>().To<BackgroundFactory>().AsSingle();
            Container.Bind<IBackgroundsAssetsProvider>().To<BackgroundsAssetsProvider>().AsSingle();
        }

        private void BindHUD()
        {
            Container.Bind<IHUDFactory>().To<HUDFactory>().AsSingle();
            Container.Bind<IHUDAssetsProvider>().To<HUDAssetsProvider>().AsSingle();
        }
    }
}