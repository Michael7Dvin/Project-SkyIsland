using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Levels
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly IWorldObjectsSpawnerProvider _worldObjectsSpawnerProvider;
        private readonly IPlayerFactory _playerFactory;
        private readonly IslandWorldData _islandWorldData;

        public IslandWorldObjectsSpawner(IWorldObjectsSpawnerProvider worldObjectsSpawnerProvider,
            IPlayerFactory playerFactory,
            IslandWorldData islandWorldData)
        {
            _worldObjectsSpawnerProvider = worldObjectsSpawnerProvider;
            _playerFactory = playerFactory;
            _islandWorldData = islandWorldData;
        }

        public LevelType LevelType => LevelType.Island;

        public void Initialize() => 
            SetSelfToProvider();

        public void SpawnWorldObjects()
        {
            Transform playerSpawnPoint = _islandWorldData.PlayerSpawnPoint;
            _playerFactory.Create(playerSpawnPoint.position, playerSpawnPoint.rotation);
        }

        private void SetSelfToProvider() => 
            _worldObjectsSpawnerProvider.Set(this);
    }
}