using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.HUD;

namespace Infrastructure.Services.AssetProviding.Providers.UI.HUD
{
    public class HUDAssetsProvider : IHUDAssetsProvider
    {
        private readonly HUDAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public HUDAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.UI.HUD;
            _addressablesLoader = addressablesLoader;
        }
        
        public async UniTask<HealthBar> LoadHealthBar() =>
            await _addressablesLoader.LoadComponent<HealthBar>(_addresses.HealthBar);
    }
}