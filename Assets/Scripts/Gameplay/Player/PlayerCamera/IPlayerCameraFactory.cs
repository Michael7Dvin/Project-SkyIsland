using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        GameObject Create(Transform parent);
    }
}