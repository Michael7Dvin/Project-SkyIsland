using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds
{
    public class BackgroundsAssetsProvider : IBackgroundsAssetsProvider
    {
        private string _mainMenuBackgroundAddress;
        private string _pauseBackgroundAddress;
        private string _deathBackgroundAddress;

        private readonly IResourcesLoader _resourcesLoader;

        public BackgroundsAssetsProvider(IStaticDataProvider staticDataProvider, IResourcesLoader resourcesLoader)
        {
            BackgroundsAssetsAddresses addresses = staticDataProvider.AssetsAddresses.UI.Backgrounds;
            _resourcesLoader = resourcesLoader;
            SetAddresses(addresses);
        }
        
        public async UniTask<GameObject> LoadMainMenu() => 
            await _resourcesLoader.Load<GameObject>(_mainMenuBackgroundAddress);

        public async UniTask<GameObject> LoadPause() => 
            await _resourcesLoader.Load<GameObject>(_pauseBackgroundAddress);
        
        public async UniTask<GameObject> LoadDeath() => 
            await _resourcesLoader.Load<GameObject>(_deathBackgroundAddress);
        
        private void SetAddresses(BackgroundsAssetsAddresses addresses)
        {
            _mainMenuBackgroundAddress = addresses.MainMenuBackground;
            _pauseBackgroundAddress = addresses.PauseBackground;
            _deathBackgroundAddress = addresses.DeathBackground;
        }
    }
}