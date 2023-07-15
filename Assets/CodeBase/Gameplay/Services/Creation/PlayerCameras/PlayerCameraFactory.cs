using Cinemachine;
using Cysharp.Threading.Tasks;
using Gameplay.MonoBehaviours.Destroyable;
using Gameplay.PlayerCameras;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.AssetProviding.Providers.ForCamera;
using Infrastructure.Services.Input;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Services.Creation.PlayerCameras
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
        private readonly ICustomLogger _logger;

        public PlayerCameraFactory(IStaticDataProvider staticDataProvider,
            ICameraAssetsProvider cameraAssetsProvider,
            ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator,
            IInputService inputService,
            ICustomLogger logger)
        {
            _config = staticDataProvider.PlayerCameraConfig;
            
            _cameraAssetsProvider = cameraAssetsProvider;
            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
            _inputService = inputService;
            _logger = logger;
        }

        public async UniTask WarmUp()
        {
            await _commonAssetsProvider.LoadEmptyGameObject();
            await _cameraAssetsProvider.LoadCamera();
            await _cameraAssetsProvider.LoadFreeLookController();
        }

        public async UniTask<PlayerCamera> Create(Transform hero)
        {
            GameObject root = await CreateCameraRoot();
            GameObject followPoint = await CreateFollowPoint(_config.FollowPointOffsetFromPlayer, hero);
            
            GameObject cameraGameObject = await CreateCamera(root.transform);

            GetComponents(cameraGameObject, out Camera camera, out IDestroyable destroyable);
            
            CinemachineFreeLook freeLookController = 
                await CreateFreeLookController(followPoint.transform, root.transform);

            PlayerCameraProgressDataProvider progressDataProvider = new(freeLookController);
            
            PlayerCameraController controller = _instantiator.Instantiate<PlayerCameraController>();
            controller.Construct(freeLookController);
            
            PlayerCamera playerCamera = new PlayerCamera(camera, controller, destroyable, progressDataProvider);
            return playerCamera;
        }

        private async UniTask<GameObject> CreateCameraRoot()
        {
            GameObject emptyPrefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject cameraRoot = _instantiator.InstantiatePrefab(emptyPrefab);
            cameraRoot.name = CameraRootName;

            return cameraRoot;
        }

        private async UniTask<GameObject> CreateFollowPoint(Vector3 offset, Transform parent)
        {
            GameObject emptyPrefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject followPoint = _instantiator.InstantiatePrefab(emptyPrefab, parent);
            followPoint.name = FollowPointName;
            followPoint.transform.localPosition = offset;
            
            return followPoint;
        }

        private async UniTask<GameObject> CreateCamera(Transform parent)
        {
            GameObject prefab = await _cameraAssetsProvider.LoadCamera();
            return _instantiator.InstantiatePrefab(prefab, parent);
        }

        private void GetComponents(GameObject cameraGameObject, out Camera camera, out IDestroyable destroyable)
        {
            if (cameraGameObject.TryGetComponent(out camera) == false)
                _logger.LogError($"{nameof(camera)} prefab have no {nameof(Camera)} attached");
            
            if (cameraGameObject.TryGetComponent(out destroyable) == false)
                _logger.LogError($"{nameof(camera)} prefab have no {nameof(IDestroyable)} attached");
        }
        

        private async UniTask<CinemachineFreeLook> CreateFreeLookController(Transform followPoint, Transform parent)
        {
            CinemachineFreeLook prefab = await _cameraAssetsProvider.LoadFreeLookController();
            
            CinemachineFreeLook controller = _instantiator.InstantiatePrefabForComponent(prefab, parent);
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