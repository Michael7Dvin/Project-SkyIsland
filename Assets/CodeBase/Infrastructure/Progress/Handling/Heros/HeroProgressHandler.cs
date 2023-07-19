using Gameplay.Heros;
using Gameplay.Services.Providers.HeroProviding;
using Infrastructure.Services.Logging;

namespace Infrastructure.Progress.Handling.Heros
{
    public class HeroProgressHandler : IHeroProgressHandler
    {
        private readonly IHeroProvider _heroProvider;
        private readonly ICustomLogger _logger;

        public HeroProgressHandler(IHeroProvider heroProvider, ICustomLogger logger)
        {
            _heroProvider = heroProvider;
            _logger = logger;
        }

        private HeroProgressDataProvider HeroProgressDataProvider
        {
            get
            {
                if (_heroProvider.Hero == null)
                    _logger.LogError($"Unable to perform operation. {nameof(IHeroProvider)} have no {nameof(Hero)} set");

                return _heroProvider.Hero.Value.ProgressDataProvider;
            }
        }

        public void WriteProgress(HeroProgress progress)
        {
            progress.IsEmpty = false;
            progress.CurrentHealth = HeroProgressDataProvider.Health;
        }
        
        public void LoadProgress(HeroProgress progress)
        {
            if (progress.IsEmpty == false)
                HeroProgressDataProvider.Health = progress.CurrentHealth;
        }
    }
}