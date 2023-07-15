using Gameplay.Heros;
using Gameplay.Services.HeroProviding;
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

        private IHeroProgressDataProvider HeroProgressDataProvider =>
            _heroProvider.Hero.HeroProgressDataProvider;

        public void WriteProgress(HeroProgress progress)
        {
            if (_heroProvider.Hero == null)
            {
                _logger.LogError($"Can't write values. {nameof(IHeroProvider)} have no {nameof(Hero)} set");
                return;
            }

            progress.CurrentHealth = HeroProgressDataProvider.Health;
            progress.IsEmpty = false;
        }
        
        public void LoadProgress(HeroProgress progress)
        {
            if (_heroProvider.Hero == null)
            {
                _logger.LogError($"Can't set values. {nameof(IHeroProvider)} have no {nameof(Hero)} set");
                return;
            }

            if (progress.IsEmpty == false)
                HeroProgressDataProvider.Health = progress.CurrentHealth;
        }
    }
}