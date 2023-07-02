using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;

namespace Infrastructure.Services.AssetProviding.Providers.UI.Windows
{
    public class WindowsAssetsProvider : IWindowsAssetsProvider
    {
        private readonly WindowsAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public WindowsAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.UI.Windows;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask<MainMenuWindowView> LoadMainMenuWindow() =>
            await _addressablesLoader.LoadComponent<MainMenuWindowView>(_addresses.MainMenuWindowView);

        public async UniTask<SaveSelectionWindowView> LoadSaveSelectionWindow() =>
            await _addressablesLoader.LoadComponent<SaveSelectionWindowView>(_addresses.SaveSelectionWindowView);

        public async UniTask<PauseWindowView> LoadPauseWindow() =>
            await _addressablesLoader.LoadComponent<PauseWindowView>(_addresses.PauseWindowView);

        public async UniTask<DeathWindowView> LoadDeathWindow() =>
            await _addressablesLoader.LoadComponent<DeathWindowView>(_addresses.DeathWindowView);
    }
}