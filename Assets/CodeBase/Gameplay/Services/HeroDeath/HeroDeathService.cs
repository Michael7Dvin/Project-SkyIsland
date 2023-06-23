using Gameplay.Dying;
using Infrastructure.Services.Logging;
using UI.Services.Operating;
using UI.Windows;

namespace Gameplay.Services.HeroDeath
{
    public class HeroDeathService : IHeroDeathService
    {
        private IDeath _playerDeath;

        private readonly IWindowsOperator _windowsOperator;
        private readonly ICustomLogger _logger;

        public HeroDeathService(IWindowsOperator windowsOperator, ICustomLogger logger)
        {
            _windowsOperator = windowsOperator;
            _logger = logger;
        }

        public void Init(IDeath playerDeath)
        {
            _playerDeath = playerDeath;
            _playerDeath.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _logger.Log("Hero Died");
            
            _playerDeath.Died -= OnPlayerDied;
            _playerDeath = null;
            _windowsOperator.OpenWindow(WindowType.Death);
        }
    }
}