using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.HUD;

namespace Infrastructure.Services.AssetProviding.Providers.UI.HUD
{
    public class HUDAssetsProvider : IHUDAssetsProvider
    {
        private string _healthBarAddress;
        
        private readonly IResourcesLoader _resourcesLoader;

        public HUDAssetsProvider(IStaticDataProvider staticDataProvider, IResourcesLoader resourcesLoader)
        {
            AllAssetsAddresses addresses = staticDataProvider.AssetsAddresses;
            _resourcesLoader = resourcesLoader;
            SetAddresses(addresses);
        }
        
        public async UniTask<HealthBar> LoadHealthBar() => 
            await _resourcesLoader.Load<HealthBar>(_healthBarAddress);
        
        private void SetAddresses(AllAssetsAddresses addresses) => 
            _healthBarAddress = addresses.UI.HUD.HealthBar;
    }
}