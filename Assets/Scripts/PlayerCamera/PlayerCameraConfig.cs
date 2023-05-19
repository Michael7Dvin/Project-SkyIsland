using Cinemachine;
using UnityEngine;

namespace PlayerCamera
{
    [CreateAssetMenu(fileName = "Player Camera Config", menuName = "Configs")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField] public Camera Camera;
        [field: SerializeField] public CinemachineFreeLook CameraController;
        [SerializeField] public Vector3 CameraFollowPointOffsetFromPlayer;
    }
}