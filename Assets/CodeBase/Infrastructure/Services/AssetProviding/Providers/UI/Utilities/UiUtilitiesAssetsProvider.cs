using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses.UI;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.Providers.UI.Utilities
{
    public class UiUtilitiesAssetsProvider : IUiUtilitiesAssetsProvider
    {
        private readonly AllUIAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public UiUtilitiesAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.UI;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask<Canvas> LoadCanvas() =>
            await _addressablesLoader.LoadComponent<Canvas>(_addresses.Canvas);

        public async UniTask<EventSystem> LoadEventSystem() =>
            await _addressablesLoader.LoadComponent<EventSystem>(_addresses.EventSystem);
    }
}