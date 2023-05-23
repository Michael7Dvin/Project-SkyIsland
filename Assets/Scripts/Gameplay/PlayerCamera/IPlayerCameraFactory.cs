using UnityEngine;

namespace Gameplay.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        GameObject Create(PlayerCameraConfig config, Transform parent);
    }
}