using UnityEngine;

namespace Gameplay.Heros
{
    public interface ISavableHero
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }

        float Health { get; set; }
    }
}