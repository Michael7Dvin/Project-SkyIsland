using UnityEngine;

namespace Gameplay.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        Camera Create(Transform hero);
    }
}