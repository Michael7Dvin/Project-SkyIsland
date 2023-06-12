using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.HUD;
using UI.Windows.Implementations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.UI
{
    public class UIAssetsProvider : IUIAssetsProvider
    {
        private string _canvasAddress;
        private string _eventSystemAddress;
        private string _healthBarAddress;
        private string _mainMenuWindowAddress;
        private string _saveSelectionWindowAddress;
        private string _pauseWindowAddress;
        private string _deathWindowAddress;
        
        private readonly IResourcesLoader _resourcesLoader;

        public UIAssetsProvider(IStaticDataProvider staticDataProvider,
            IResourcesLoader resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;

            AllAssetsAddresses addresses = staticDataProvider.AllAssetsAddresses;
            SetAddresses(addresses);
        }

        public async UniTask<Canvas> LoadCanvas() => 
            await _resourcesLoader.Load<Canvas>(_canvasAddress);

        public async UniTask<EventSystem> LoadEventSystem() => 
            await _resourcesLoader.Load<EventSystem>(_eventSystemAddress);
        
        public async UniTask<MainMenuWindow> LoadMainMenuWindow() => 
            await _resourcesLoader.Load<MainMenuWindow>(_mainMenuWindowAddress);

        public async UniTask<SaveSelectionWindow> LoadSaveSelectionWindow() => 
            await _resourcesLoader.Load<SaveSelectionWindow>(_saveSelectionWindowAddress);

        public async UniTask<PauseWindow> LoadPauseWindow() => 
            await _resourcesLoader.Load<PauseWindow>(_pauseWindowAddress);

        public async UniTask<DeathWindow> LoadDeathWindow() => 
            await _resourcesLoader.Load<DeathWindow>(_deathWindowAddress);

        public async UniTask<HealthBar> LoadHealthBar() => 
            await _resourcesLoader.Load<HealthBar>(_healthBarAddress);

        private void SetAddresses(AllAssetsAddresses addresses)
        {
            _canvasAddress = addresses.UIAssets.Canvas;
            _eventSystemAddress = addresses.UIAssets.EventSystem;
            
            _mainMenuWindowAddress = addresses.UIAssets.MainMenuWindow;
            _saveSelectionWindowAddress = addresses.UIAssets.SaveSelectionWindow;
            _pauseWindowAddress = addresses.UIAssets.PauseWindow;
            _deathWindowAddress = addresses.UIAssets.DeathWindow;
            
            _healthBarAddress = addresses.UIAssets.HealthBar;
        }
    }
}