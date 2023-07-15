using UnityEngine;

namespace Infrastructure.LevelLoading.Data
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