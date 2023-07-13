using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.Services.Creation.Heros.Factory;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.LevelServicesProviding;
using UnityEngine;

namespace Infrastructure.Services.LevelLoading.WorldObjectsSpawning
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IslandWorldData _worldData;
        private readonly IHeroFactory _heroFactory;
        private readonly HeroProgressHandler _heroProgressHandler;
        private readonly IslandLevelProgressHandler _islandLevelProgressHandler;

        public IslandWorldObjectsSpawner(ILevelServicesProvider levelServicesProvider,
            IslandWorldData worldData,
            IHeroFactory heroFactory,
            HeroProgressHandler heroProgressHandler,
            IslandLevelProgressHandler islandLevelProgressHandler)
        {
            _levelServicesProvider = levelServicesProvider;
            _worldData = worldData;
            _heroFactory = heroFactory;
            _heroProgressHandler = heroProgressHandler;
            _islandLevelProgressHandler = islandLevelProgressHandler;
        }

        public LevelType LevelType => LevelType.Island;

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            await SpawnHero();
        }

        private async UniTask SpawnHero()
        {
            if (_heroProgressHandler.Hero == null)
            {
                Vector3 position = _worldData.HeroSpawnPoint.position;
                Quaternion rotation = _worldData.HeroSpawnPoint.rotation;
                Hero hero = await _heroFactory.Create(position, rotation);
                
                _heroProgressHandler.RegisterHero(hero);
                _islandLevelProgressHandler.RegisterHero(hero);
            }
        }

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetWorldObjectsSpawner(this);
    }
}