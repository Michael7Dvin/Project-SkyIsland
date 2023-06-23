using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.Providers.UI.All
{
    public class UIAssetsProvider : IUIAssetsProvider
    {
        private string _canvasAddress;
        private string _eventSystemAddress;

        private readonly IResourcesLoader _resourcesLoader;

        public UIAssetsProvider(IStaticDataProvider staticDataProvider,
            IResourcesLoader resourcesLoader)
        {
            AllAssetsAddresses addresses = staticDataProvider.AssetsAddresses;
            _resourcesLoader = resourcesLoader;
            SetAddresses(addresses);
        }

        public async UniTask<Canvas> LoadCanvas() => 
            await _resourcesLoader.Load<Canvas>(_canvasAddress);

        public async UniTask<EventSystem> LoadEventSystem() => 
            await _resourcesLoader.Load<EventSystem>(_eventSystemAddress);
        
        
        private void SetAddresses(AllAssetsAddresses addresses)
        {
            _canvasAddress = addresses.UI.Canvas;
            _eventSystemAddress = addresses.UI.EventSystem;
        }
    }
}