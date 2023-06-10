using Gameplay.Services.Pause;
using Infrastructure.Services.Input.Service;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Gameplay.Services.PlayerPausing
{
    public class PlayerPause : IPlayerPause
    {
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        private readonly IWindowsService _windowsService;

        public PlayerPause(IInputService inputService, IPauseService pauseService, IWindowsService windowsService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
            _windowsService = windowsService;
        }

        public void Dispose() => 
            _inputService.Utility.Paused -= OnPausedInput;
        
        public void Initialize() => 
            _inputService.Utility.Paused += OnPausedInput;
        
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

        private void Pause()
        {
            _pauseService.Pause();
            _inputService.Camera.Disable();
            _windowsService.OpenWindow(WindowType.Pause);
        }
    }
}