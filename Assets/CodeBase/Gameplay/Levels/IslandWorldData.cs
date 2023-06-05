using UnityEngine;

namespace Gameplay.Levels
{
    public class IslandWorldData
    {
        public IslandWorldData(Transform playerSpawnPoint)
        {
            PlayerSpawnPoint = playerSpawnPoint;
        }

        public readonly Transform PlayerSpawnPoint;
    }
}