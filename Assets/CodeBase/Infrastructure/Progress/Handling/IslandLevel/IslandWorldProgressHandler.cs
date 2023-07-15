using Gameplay.Heros;
using Gameplay.Services.HeroProviding;
using Infrastructure.Services.Logging;

namespace Infrastructure.Progress.Handling.IslandLevel
{
    public class IslandWorldProgressHandler : IIslandWorldProgressHandler
    {
        private readonly IHeroProvider _heroProvider;
        private readonly ICustomLogger _logger;

        public IslandWorldProgressHandler(IHeroProvider heroProvider, ICustomLogger logger)
        {
            _heroProvider = heroProvider;
            _logger = logger;
        }
        
        private IHeroProgressDataProvider HeroProgressDataProvider =>
            _heroProvider.Hero.HeroProgressDataProvider;

        public void WriteProgress(IslandWorldProgress progress)
        {
            if (_heroProvider.Hero == null)
            {
                _logger.LogError($"Can't write values. {nameof(IHeroProvider)} have no {nameof(Hero)} set");
                return;
            }
            
            progress.IsEmpty = false;

            progress.HeroPosition = HeroProgressDataProvider.Position;
            progress.HeroRotation = HeroProgressDataProvider.Rotation;
        }

        public void LoadProgress(IslandWorldProgress progress)
        {
            if (_heroProvider.Hero == null)
            {
                _logger.LogError($"Can't write values. {nameof(IHeroProvider)} have no {nameof(Hero)} set");
                return;
            }
            
            if (progress.IsEmpty == false)
            {
                HeroProgressDataProvider.Position = progress.HeroPosition;
                HeroProgressDataProvider.Rotation = progress.HeroRotation;
            }
        }
    }
}