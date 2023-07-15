using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.HeroDeath;
using Gameplay.Services.HeroProviding;
using Infrastructure.LevelLoading.Data;
using Infrastructure.LevelLoading.LevelServicesProviding;
using UnityEngine;

namespace Infrastructure.LevelLoading.WorldObjectsSpawning
{
    public class IslandWorldObjectsSpawner : IWorldObjectsSpawner
    {
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly IslandWorldData _worldData;
        private readonly IHeroFactory _heroFactory;
        private readonly IHeroProvider _heroProvider;
        private readonly IHeroDeathService _heroDeathService;

        public IslandWorldObjectsSpawner(ILevelServicesProvider levelServicesProvider,
            IslandWorldData worldData,
            IHeroFactory heroFactory,
            IHeroProvider heroProvider,
            IHeroDeathService heroDeathService)
        {
            _levelServicesProvider = levelServicesProvider;
            _worldData = worldData;
            _heroFactory = heroFactory;
            _heroProvider = heroProvider;
            _heroDeathService = heroDeathService;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public async UniTask SpawnWorldObjects()
        {
            await SpawnHero();
        }

        private async UniTask SpawnHero()
        {
            if (_heroProvider.Hero == null)
            {
                Vector3 position = _worldData.HeroSpawnPoint.position;
                Quaternion rotation = _worldData.HeroSpawnPoint.rotation;
                Hero hero = await _heroFactory.Create(position, rotation);
                
                _heroProvider.Set(hero);
                _heroDeathService.Init(hero.Death);
            }
        }

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetWorldObjectsSpawner(this);
    }
}