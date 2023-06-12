using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using UI.Windows;
using UI.Windows.Base;
using UI.Windows.Factory;

namespace UI.Services.WindowsOperating
{
    public class WindowsService : IWindowsService
    {
        private readonly List<IWindow> _openedWindows = new();

        private readonly IWindowFactory _windowFactory;
        private readonly ICustomLogger _logger;

        public WindowsService(ICustomLogger logger, IWindowFactory windowFactory)
        {
            _logger = logger;
            _windowFactory = windowFactory;
        }
        
        public async UniTask<IWindow> OpenWindow(WindowType type)
        {
            IWindow window;
            
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
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            AddOpenedWindow(window);
            return window;
        }

        public void CloseWindow(WindowType type)
        {
            IWindow window = _openedWindows.FirstOrDefault(window => window.Type == type);

            if (window == null)
            {
                _logger.LogWarning($"{nameof(IWindow)} of {nameof(WindowType)}: '{type}' not found");
                return;
            }

            window.Close();
        }

        private void AddOpenedWindow(IWindow window)
        {
            window.Closed += RemoveClosedWindow;
            _openedWindows.Add(window);
        }

        private void RemoveClosedWindow(IWindow window)
        {
            window.Closed -= RemoveClosedWindow;
            _openedWindows.Remove(window);
        }
    }
}