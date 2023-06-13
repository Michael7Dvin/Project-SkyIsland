using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Pause;
using UI.Services.WindowsOperating;
using UI.Windows;
using UI.Windows.Base;

namespace Gameplay.Services.PlayerPausing
{
    public class PlayerPause : IPlayerPause
    {
        private IWindow _currentPauseWindow;
        
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        private readonly IWindowsService _windowsService;

        public PlayerPause(IInputService inputService, IPauseService pauseService, IWindowsService windowsService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
            _windowsService = windowsService;
        }

        public void Initialize() => 
            _inputService.Utility.Paused += OnPausedInput;

        public void Dispose()
        {
            _inputService.Utility.Paused -= OnPausedInput;

            if (_currentPauseWindow != null) 
                _currentPauseWindow.Destroyed -= OnPauseWindowClosed;
        }

        private void OnPausedInput()
        {
            if (_pauseService.Paused.Value == true)
                Resume();
            else
                Pause();
        }

        private void Resume()
        {
            _pauseService.Resume();
            _inputService.Camera.Enable();
            _windowsService.CloseWindow(WindowType.Pause);
        }

        private async void Pause()
        {
            _pauseService.Pause();
            _inputService.Camera.Disable();
            _currentPauseWindow = await _windowsService.OpenWindow(WindowType.Pause);

            _currentPauseWindow.Destroyed += OnPauseWindowClosed;
        }

        private void OnPauseWindowClosed(IWindow pauseWindow)
        {
            _currentPauseWindow = null;
            pauseWindow.Destroyed -= OnPauseWindowClosed;
            _pauseService.Resume();
            _inputService.Camera.Enable();
        }
    }
}