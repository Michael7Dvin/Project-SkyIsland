using Cinemachine;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Common;
using Infrastructure.Services.AssetProviding.ForCamera;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.PlayerCamera
{
    public class PlayerCameraFactory : IPlayerCameraFactory
    {
        private const string CameraRootName = "Player Camera";
        private const string FollowPointName = "Follow Point";

        private readonly PlayerCameraConfig _config;

        private readonly ICameraAssetsProvider _cameraAssetsProvider;
        private readonly ICommonAssetsProvider _commonAssetsProvider;
        
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;
        
        public PlayerCameraFactory(IStaticDataProvider staticDataProvider,
            ICameraAssetsProvider cameraAssetsProvider,
            ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator,
            IInputService inputService)
        {
            _config = staticDataProvider.PlayerCameraConfig;
            
            _cameraAssetsProvider = cameraAssetsProvider;
            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
            _inputService = inputService;
        }

        public async UniTask WarmUp()
        {
            await _commonAssetsProvider.LoadEmptyGameObject();
            await _cameraAssetsProvider.LoadCamera();
            await _cameraAssetsProvider.LoadFreeLookController();
        }

        public async UniTask<Camera> Create(Transform hero)
        {
            GameObject root = await CreateCameraRoot();
            Camera camera = await CreateCamera(root.transform);
            GameObject followPoint = await CreateFollowPoint(_config.FollowPointOffsetFromPlayer, hero);
            
            await CreateFreeLookController(followPoint.transform, root.transform);

            return camera;
        }

        private async UniTask<GameObject> CreateCameraRoot()
        {
            GameObject emptyPrefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject cameraRoot = _instantiator.Instantiate(emptyPrefab);
            cameraRoot.name = CameraRootName;

            return cameraRoot;
        }

        private async UniTask<Camera> CreateCamera(Transform parent)
        {
            Camera cameraPrefab = await _cameraAssetsProvider.LoadCamera();
            return _instantiator.Instantiate(cameraPrefab, parent);
        }

        private async UniTask<GameObject> CreateFollowPoint(Vector3 offset, Transform parent)
        {
            GameObject emptyPrefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject followPoint = _instantiator.Instantiate(emptyPrefab, parent);
            followPoint.name = FollowPointName;
            followPoint.transform.localPosition = offset;
            
            return followPoint;
        }

        private async UniTask<CinemachineFreeLook> CreateFreeLookController(Transform followPoint, Transform parent)
        {
            CinemachineFreeLook prefab = await _cameraAssetsProvider.LoadFreeLookController();
            
            CinemachineFreeLook controller = _instantiator.Instantiate(prefab, parent);
            controller.Follow = followPoint;
            controller.LookAt = followPoint;

            SetUpFreeLookControllerInput(controller);

            return controller;
        }

        private void SetUpFreeLookControllerInput(CinemachineFreeLook controller)
        {
            CinemachineInputProvider inputProvider = controller.GetComponent<CinemachineInputProvider>();
            InputAction orbitalRotation = _inputService.Camera.OrbitalRotation;

            inputProvider.XYAxis.Set(orbitalRotation);
            inputProvider.ZAxis.Set(orbitalRotation);
        }
    }
}