using Cysharp.Threading.Tasks;
using Gameplay.Services.Factories.HeroFactory;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.LevelServicesProviding;
using UnityEngine;

namespace Infrastructure.Services.LevelLoading.WorldObjectsSpawning
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IHeroFactory _heroFactory;
        private readonly IslandWorldData _islandWorldData;

        public IslandWorldObjectsSpawner(ILevelServicesProvider levelServicesProvider,
            IHeroFactory heroFactory,
            IslandWorldData islandWorldData)
        {
            _levelServicesProvider = levelServicesProvider;
            _heroFactory = heroFactory;
            _islandWorldData = islandWorldData;
        }

        public LevelType LevelType => LevelType.Island;

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            Transform heroSpawnPoint = _islandWorldData.PlayerSpawnPoint;
            await _heroFactory.Create(heroSpawnPoint.position, heroSpawnPoint.rotation);
        }

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetWorldObjectsSpawner(this);
    }
}