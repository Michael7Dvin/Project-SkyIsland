using UnityEngine;

namespace Infrastructure.Services.LevelLoading.Data
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