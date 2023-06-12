using UnityEngine;

namespace Infrastructure.LevelLoading
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