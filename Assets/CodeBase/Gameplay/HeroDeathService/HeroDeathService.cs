using Gameplay.Dying;
using Infrastructure.Services.Input.Handlers;
using Infrastructure.Services.Input.Service;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Gameplay.HeroDeathService
{
    public class HeroDeathService : IHeroDeathService
    {
        private IDeath _playerDeath;
        
        private readonly IWindowsService _windowsService;
        private readonly IInputService _inputService;
        
        public HeroDeathService(IWindowsService windowsService, IInputService inputService)
        {
            _windowsService = windowsService;
            _inputService = inputService;
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

            _inputService.DisableInput(InputHandlerType.Camera); 
            _windowsService.OpenWindow(WindowType.Death);
        }
    }
}