using UnityEngine;

namespace Gameplay.Hero
{
    public interface IHeroFactory
    {
        Hero Create(Vector3 position, Quaternion rotation);
    }
}