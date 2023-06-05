using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerFactory
    {
        Player Create(Vector3 position, Quaternion rotation);
    }
}