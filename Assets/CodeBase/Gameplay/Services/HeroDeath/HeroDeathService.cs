using Gameplay.Dying;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Gameplay.Services.HeroDeath
{
    public class HeroDeathService : IHeroDeathService
    {
        private IDeath _playerDeath;
        
        private readonly IWindowsService _windowsService;

        public HeroDeathService(IWindowsService windowsService)
        {
            _windowsService = windowsService;
        }

        public void Init(IDeath playerDeath)
        {
            _playerDeath = playerDeath;
            _playerDeath.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _playerDeath.Died -= OnPlayerDied;
            _playerDeath = null;
            _windowsService.OpenWindow(WindowType.Death);
        }
    }
}