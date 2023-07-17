using UnityEngine;

namespace Infrastructure.LevelLoading.WorldData
{
    public class IslandWorldData
    {
        public IslandWorldData(Transform heroSpawnPoint)
        {
            HeroSpawnPoint = heroSpawnPoint;
        }

        public readonly Transform HeroSpawnPoint;
    }
}