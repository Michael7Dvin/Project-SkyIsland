using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds
{
    public class BackgroundsAssetsProvider : IBackgroundsAssetsProvider
    {
        private readonly BackgroundsAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public BackgroundsAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.UI.Backgrounds;
            _addressablesLoader = addressablesLoader;
        }
        
        public async UniTask<GameObject> LoadMainMenu() => 
            await _addressablesLoader.LoadGameObject(_addresses.MainMenuBackground);

        public async UniTask<GameObject> LoadPause() => 
            await _addressablesLoader.LoadGameObject(_addresses.PauseBackground);
        
        public async UniTask<GameObject> LoadDeath() => 
            await _addressablesLoader.LoadGameObject(_addresses.DeathBackground);
    }
}