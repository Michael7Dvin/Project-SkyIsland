using Cinemachine;
using UnityEngine;

namespace PlayerCamera
{
    public class PlayerCameraFactory : IPlayerCameraFactory
    {
        private const string CameraGameobjectName = "Player Camera";
        private const string CameraFollowPointName = "Camera Follow Point";
        
        public GameObject Create(PlayerCameraConfig config, Transform parent)
        {
            GameObject camera = Object.Instantiate(new GameObject(CameraGameobjectName), parent);
            Transform transform = camera.transform;
            
            CreateCamera(config.Camera, transform);
            Transform followPointTransform = CreateFollowPoint(config.CameraFollowPointOffsetFromPlayer, transform).transform;
            CreateCameraController(config.CameraController, followPointTransform, transform);

            return camera;
        }

        private Camera CreateCamera(Camera cameraPrefab, Transform parent) =>
             Object.Instantiate(cameraPrefab, parent);

        private GameObject CreateFollowPoint(Vector3 offset, Transform parent)
        {
            GameObject followPoint = Object.Instantiate(new GameObject(CameraFollowPointName), parent);
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