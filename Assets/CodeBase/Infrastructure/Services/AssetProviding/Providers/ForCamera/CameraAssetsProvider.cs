using Cinemachine;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.ForCamera
{
    public class CameraAssetsProvider : ICameraAssetsProvider
    {
        private readonly CameraAssetsAddresses _addresses;
        private readonly IAddressablesLoader _addressablesLoader;

        public CameraAssetsProvider(IStaticDataProvider staticDataProvider, IAddressablesLoader addressablesLoader)
        {
            _addresses = staticDataProvider.AssetsAddresses.Camera;
            _addressablesLoader = addressablesLoader;
        }

        public async UniTask<Camera> LoadCamera() =>
             await _addressablesLoader.LoadComponent<Camera>(_addresses.Camera);
        
        public async UniTask<CinemachineFreeLook> LoadFreeLookController() =>
            await _addressablesLoader.LoadComponent<CinemachineFreeLook>(_addresses.FreeLookController);
    }
}