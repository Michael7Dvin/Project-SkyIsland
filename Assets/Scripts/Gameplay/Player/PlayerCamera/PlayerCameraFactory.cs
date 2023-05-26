using Cinemachine;
using Infrastructure.Services.Configuration;
using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    public class PlayerCameraFactory : IPlayerCameraFactory
    {
        private readonly PlayerCameraConfig _config;

        public PlayerCameraFactory(IConfigProvider configProvider)
        {
            _config = configProvider.GetForPlayer().Camera;
        }

        public Camera Create(Transform parent)
        {
            GameObject cameraGameObject = Object.Instantiate(_config.EmptyCameraParent, parent);
            Transform transform = cameraGameObject.transform;
            
            Camera camera = CreateCamera(_config.Camera, transform);
            Transform followPointTransform = CreateFollowPoint(_config.FollowPointOffsetFromPlayer, transform).transform;
            CreateCameraController(_config.Controller, followPointTransform, transform);

            return camera;
        }

        private Camera CreateCamera(Camera cameraPrefab, Transform parent) =>
             Object.Instantiate(cameraPrefab, parent);

        private GameObject CreateFollowPoint(Vector3 offset, Transform parent)
        {
            GameObject followPoint = Object.Instantiate(_config.EmptyFollowPoint, parent);
            followPoint.transform.localPosition = offset;
            return followPoint;
        }

        private CinemachineFreeLook CreateCameraController(CinemachineFreeLook controllerPrefab, Transform followPoint, Transform parent)
        {
            CinemachineFreeLook controller = Object.Instantiate(controllerPrefab, parent);
            controller.Follow = followPoint;
            controller.LookAt = followPoint;

            return controller;
        }
    }
}