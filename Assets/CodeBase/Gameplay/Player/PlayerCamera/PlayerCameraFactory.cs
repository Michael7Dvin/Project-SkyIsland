using Cinemachine;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    public class PlayerCameraFactory : IPlayerCameraFactory
    {
        private readonly PlayerCameraConfig _config;
        private readonly IInstantiator _instantiator;
        
        public PlayerCameraFactory(IStaticDataProvider staticDataProvider, IInstantiator instantiator)
        {
            _config = staticDataProvider.GetPlayerConfig().Camera;
            _instantiator = instantiator;
        }

        public Camera Create(Transform parent)
        {
            GameObject rootGameObject = _instantiator.Instantiate(_config.Root, parent);
            Transform root = rootGameObject.transform;
            
            Camera camera = CreateCamera(_config.Camera, root);
            Transform followPointTransform = CreateFollowPoint(_config.FollowPointOffsetFromPlayer, root).transform;
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

            return controller;
        }
    }
}