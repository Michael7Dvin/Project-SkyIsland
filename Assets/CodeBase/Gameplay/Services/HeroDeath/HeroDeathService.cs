using Gameplay.Dying;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Gameplay.Services.HeroDeath
{
    public class HeroDeathService : IHeroDeathService
    {
        private IDeath _playerDeath;
        
        private readonly IWindowsOperator _windowsOperator;

        public HeroDeathService(IWindowsOperator windowsOperator)
        {
            _windowsOperator = windowsOperator;
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
            _windowsOperator.OpenWindow(WindowType.Death);
        }
    }
}