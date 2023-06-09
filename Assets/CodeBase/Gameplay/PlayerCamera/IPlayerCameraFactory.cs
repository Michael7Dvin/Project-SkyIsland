using UnityEngine;

namespace Gameplay.Hero.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        Camera Create(Transform hero);
    }
}