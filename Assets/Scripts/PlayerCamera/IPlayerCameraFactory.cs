using UnityEngine;

namespace PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        GameObject Create(PlayerCameraConfig config, Transform parent);
    }
}