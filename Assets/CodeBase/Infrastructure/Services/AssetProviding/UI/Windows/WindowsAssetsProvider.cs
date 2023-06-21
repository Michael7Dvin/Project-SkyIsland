using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.UI.Windows
{
    public class WindowsAssetsProvider : IWindowsAssetsProvider 
    {
        private string _mainMenuWindowAddress;
        private string _saveSelectionWindowAddress;
        private string _pauseWindowAddress;
        private string _deathWindowAddress;
        
        private readonly IResourcesLoader _resourcesLoader;

        public WindowsAssetsProvider(IStaticDataProvider staticDataProvider, IResourcesLoader resourcesLoader)
        {
            WindowsAssetsAddresses addresses = staticDataProvider.AssetsAddresses.UI.Windows;
            _resourcesLoader = resourcesLoader;
            SetAddresses(addresses);
        }

        public async UniTask<MainMenuWindowView> LoadMainMenuWindow() => 
            await _resourcesLoader.Load<MainMenuWindowView>(_mainMenuWindowAddress);

        public async UniTask<SaveSelectionWindowView> LoadSaveSelectionWindow() => 
            await _resourcesLoader.Load<SaveSelectionWindowView>(_saveSelectionWindowAddress);

        public async UniTask<PauseWindowView> LoadPauseWindow() => 
            await _resourcesLoader.Load<PauseWindowView>(_pauseWindowAddress);

        public async UniTask<DeathWindowView> LoadDeathWindow() => 
            await _resourcesLoader.Load<DeathWindowView>(_deathWindowAddress);
        
        private void SetAddresses(WindowsAssetsAddresses addresses)
        {
            _mainMenuWindowAddress = addresses.MainMenuWindow;
            _saveSelectionWindowAddress = addresses.SaveSelectionWindow;
            _pauseWindowAddress = addresses.PauseWindow;
            _deathWindowAddress = addresses.DeathWindow;
        }
    }
}