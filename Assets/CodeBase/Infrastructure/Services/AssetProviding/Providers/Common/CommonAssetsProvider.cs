using Cysharp.Threading.Tasks;
using Gameplay.MonoBehaviours.Destroyable;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.Common
{
    public class CommonAssetsProvider : ICommonAssetsProvider
    {
        private string _emptyGameObjectAddress;
        private string _heroAddress;
        
        private readonly IResourcesLoader _resourcesLoader;

        public CommonAssetsProvider(IResourcesLoader resourcesLoader, IStaticDataProvider staticDataProvider)
        {
            _resourcesLoader = resourcesLoader;
            
            AllAssetsAddresses addresses = staticDataProvider.AssetsAddresses;
            SetAddresses(addresses);
        }

        public async UniTask<GameObject> LoadEmptyGameObject() => 
            await _resourcesLoader.Load<GameObject>(_emptyGameObjectAddress);

        public async UniTask<Destroyable> LoadHero() => 
            await _resourcesLoader.Load<Destroyable>(_heroAddress);

        private void SetAddresses(AllAssetsAddresses addresses)
        {
            _emptyGameObjectAddress = addresses.Common.EmptyGameObject;
            _heroAddress = addresses.Common.Hero;
        }
    }
}