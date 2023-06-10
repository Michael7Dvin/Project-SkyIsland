using Gameplay.Hero;
using UnityEngine;

namespace Gameplay.Levels.WorldObjectsSpawning
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly IWorldObjectsSpawnerProvider _worldObjectsSpawnerProvider;
        private readonly IHeroFactory _heroFactory;
        private readonly IslandWorldData _islandWorldData;

        public IslandWorldObjectsSpawner(IWorldObjectsSpawnerProvider worldObjectsSpawnerProvider,
            IHeroFactory heroFactory,
            IslandWorldData islandWorldData)
        {
            _worldObjectsSpawnerProvider = worldObjectsSpawnerProvider;
            _heroFactory = heroFactory;
            _islandWorldData = islandWorldData;
        }

        public LevelType LevelType => LevelType.Island;

        public void Initialize() => 
            SetSelfToProvider();

        public void SpawnWorldObjects()
        {
            Transform heroSpawnPoint = _islandWorldData.PlayerSpawnPoint;
            _heroFactory.Create(heroSpawnPoint.position, heroSpawnPoint.rotation);
        }

        private void SetSelfToProvider() => 
            _worldObjectsSpawnerProvider.Set(this);
    }
}