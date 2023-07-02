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
        private readonly CommonAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public CommonAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.Common;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask<GameObject> LoadEmptyGameObject() => 
            await _addressablesLoader.LoadGameObject(_addresses.EmptyGameObject);

        public async UniTask<Destroyable> LoadHero() => 
            await _addressablesLoader.LoadComponent<Destroyable>(_addresses.Hero);
    }
}