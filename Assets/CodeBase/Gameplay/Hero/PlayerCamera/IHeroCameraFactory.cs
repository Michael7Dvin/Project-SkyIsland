using UnityEngine;

namespace Gameplay.Hero.PlayerCamera
{
    public interface IHeroCameraFactory
    {
        Camera Create(Transform parent);
    }
}