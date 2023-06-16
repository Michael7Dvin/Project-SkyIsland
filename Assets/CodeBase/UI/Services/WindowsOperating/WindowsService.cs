using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using UI.Windows;
using UI.Windows.Base.Window;
using UI.Windows.Factory;

namespace UI.Services.WindowsOperating
{
    public class WindowsService : IWindowsService
    {
        private readonly List<IWindow> _cachedWindows = new();

        private readonly IWindowFactory _windowFactory;
        private readonly ICustomLogger _logger;

        public WindowsService(ICustomLogger logger, IWindowFactory windowFactory)
        {
            _logger = logger;
            _windowFactory = windowFactory;
        }
        
        public async UniTask<IWindow> OpenWindow(WindowType type)
        {
            if (TryFindWindowInCache(type, false, out IWindow window))
            {
                window.Open();
                return window;
            }
            
            switch (type)
            {
                case WindowType.MainMenu:
                    window = await _windowFactory.CreateMainMenuWindow();
                    break;
                case WindowType.SaveSelection:
                    window = await _windowFactory.CreateSaveSelectionWindow();
                    break;
                case WindowType.Pause:
                    window = await _windowFactory.CreatePauseWindow();
                    break;
                case WindowType.Death:
                    window = await _windowFactory.CreateDeathWindow();
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(WindowType)}: '{type}'");
                    return null;
            }
            
            window.Open();
            AddToCache(window);
            return window;
        }

        public void CloseWindow(WindowType type)
        {
            if (TryFindWindowInCache(type, true, out IWindow window))
                window.Close();
            else
                _logger.LogWarning($"{nameof(IWindow)} of {nameof(WindowType)}: '{type}' not found");
        }

        private bool TryFindWindowInCache(WindowType windowType, bool isWindowOpen, out IWindow window)
        {
            window = _cachedWindows.FirstOrDefault(window =>
                window.Type == windowType && window.IsOpen.Value == isWindowOpen);
            
            return window != null;
        }

        private void AddToCache(IWindow window)
        {
            window.Destroyed += Remove;
            _cachedWindows.Add(window);

            void Remove()
            {
                window.Destroyed -= Remove;
                _cachedWindows.Remove(window);
            }
        }
    }
}