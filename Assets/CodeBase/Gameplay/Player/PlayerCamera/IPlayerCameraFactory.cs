using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        Camera Create(Transform parent);
    }
}