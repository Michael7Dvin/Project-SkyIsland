using Cinemachine;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.PlayerCamera
{
    public class PlayerCameraFactory : IPlayerCameraFactory
    {
        private readonly PlayerCameraConfig _config;
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;
        
        public PlayerCameraFactory(IStaticDataProvider staticDataProvider, IInstantiator instantiator, IInputService inputService)
        {
            _config = staticDataProvider.GetPlayerConfig().Camera;
            _instantiator = instantiator;
            _inputService = inputService;
        }

        public Camera Create(Transform hero)
        {
            GameObject rootGameObject = _instantiator.Instantiate(_config.Root);
            Transform root = rootGameObject.transform;
            
            Camera camera = CreateCamera(_config.Camera, root);
            Transform followPointTransform = CreateFollowPoint(_config.FollowPointOffsetFromPlayer, hero).transform;
            CreateCameraController(_config.Controller, followPointTransform, root);

            return camera;
        }

        private Camera CreateCamera(Camera cameraPrefab, Transform parent) =>
            _instantiator.Instantiate(cameraPrefab, parent);

        private GameObject CreateFollowPoint(Vector3 offset, Transform parent)
        {
            GameObject followPoint = _instantiator.Instantiate(_config.FollowPoint, parent);
            followPoint.transform.localPosition = offset;
            return followPoint;
        }

        private CinemachineFreeLook CreateCameraController(CinemachineFreeLook controllerPrefab, Transform followPoint, Transform parent)
        {
            CinemachineFreeLook controller = _instantiator.Instantiate(controllerPrefab, parent);
            controller.Follow = followPoint;
            controller.LookAt = followPoint;

            CinemachineInputProvider inputProvider = controller.GetComponent<CinemachineInputProvider>();
            InputAction orbitalRotation = _inputService.Camera.OrbitalRotation;
            
            inputProvider.XYAxis.Set(orbitalRotation); 
            inputProvider.ZAxis.Set(orbitalRotation);

            return controller;
        }
    }
}