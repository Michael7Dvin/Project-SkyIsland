using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Logging;
using Infrastructure.Services.Pause;
using UI.Services.WindowsOperating;
using UI.Windows;
using UI.Windows.Base.Window;

namespace Gameplay.Services.PlayerPausing
{
    public class PlayerPause : IPlayerPause
    {
        private IWindow _currentPauseWindow;
        
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        
        private readonly IWindowsService _windowsService;
        private readonly ICustomLogger _logger;
        
        public PlayerPause(IInputService inputService,
            IPauseService pauseService,
            IWindowsService windowsService,
            ICustomLogger logger)
        {
            _inputService = inputService;
            _pauseService = pauseService;
            
            _windowsService = windowsService;
            _logger = logger;
        }

        public void Initialize() => 
            _inputService.Utility.Paused += OnPausedInput;

        public void Dispose() => 
            _inputService.Utility.Paused -= OnPausedInput;

        private void OnPausedInput()
        {
            if (_pauseService.Paused.Value == true)
                Resume();
            else
                Pause();
        }

        private void Resume()
        {
            _logger.Log("Game Resumed");
            _pauseService.Resume();
            _inputService.Camera.Enable();
            _currentPauseWindow.IsOpen.Changed -= OnPauseWindowIsOpenChanged;
            _windowsService.CloseWindow(WindowType.Pause);
        }

        private async void Pause()
        {
            _logger.Log("Game Paused");
            _pauseService.Pause();
            _inputService.Camera.Disable();
            _currentPauseWindow = await _windowsService.OpenWindow(WindowType.Pause);
            _currentPauseWindow.IsOpen.Changed += OnPauseWindowIsOpenChanged;
        }

        private void OnPauseWindowIsOpenChanged(bool isOpen)
        {
            if (isOpen == false)
            {
                _logger.Log("Game Resumed");
                _currentPauseWindow.IsOpen.Changed -= OnPauseWindowIsOpenChanged;
                _pauseService.Resume();
                _inputService.Camera.Enable();
            }
        }
    }
}