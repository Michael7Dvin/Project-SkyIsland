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
        private string _cameraAddress;
        private string _freeLookControllerAddress;
        
        private readonly IResourcesLoader _resourcesLoader;

        public CameraAssetsProvider(IStaticDataProvider staticDataProvider, IResourcesLoader resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;

            AllAssetsAddresses addresses = staticDataProvider.AssetsAddresses;
            SetAddresses(addresses);
        }

        public async UniTask<Camera> LoadCamera() => 
            await _resourcesLoader.Load<Camera>(_cameraAddress);

        public async UniTask<CinemachineFreeLook> LoadFreeLookController() => 
            await _resourcesLoader.Load<CinemachineFreeLook>(_freeLookControllerAddress);

        private void SetAddresses(AllAssetsAddresses addresses)
        {
            _cameraAddress = addresses.Camera.Camera;
            _freeLookControllerAddress = addresses.Camera.FreeLookController;
        }
    }
}