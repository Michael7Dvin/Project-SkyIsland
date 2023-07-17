using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using Gameplay.Services.Factories.Heros;
using Gameplay.Services.Providers.HeroProviding;
using Infrastructure.Services.Logging;
using UnityEngine;

namespace Gameplay.Services.Spawners.Heros
{
    public class HeroSpawner : IHeroSpawner
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IHeroProvider _heroProvider;
        private readonly ICustomLogger _logger;

        public HeroSpawner(IHeroFactory heroFactory, IHeroProvider heroProvider, ICustomLogger logger)
        {
            _heroFactory = heroFactory;
            _heroProvider = heroProvider;
            _logger = logger;
        }

        public async UniTask<Hero> Spawn(Vector3 position, Quaternion rotation)
        {
            if (_heroProvider.Hero.Value != null) 
                _logger.LogWarning($"Possible duplication. {nameof(IHeroProvider)} have another {nameof(Hero)} instance set");
            
            Hero hero = await _heroFactory.Create(position, rotation);
            _heroProvider.Set(hero);

            return hero;
        }
    }
}