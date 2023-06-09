using System;
using UI.Services.Factory;
using UI.Windows;

namespace UI.Services.WindowsOperating
{
    public class WindowsService : IWindowsService
    {
        private readonly IWindowFactory _windowFactory;

        public WindowsService(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void OpenWindow(WindowType type)
        {
            switch (type)
            {
                case WindowType.MainMenu:
                    _windowFactory.CreateMainMenuWindow();
                    break;
                case WindowType.SaveSlotSelection:
                    _windowFactory.CreateSaveSelectionWindow();
                    break;
                case WindowType.Pause:
                    _windowFactory.CreatePauseWindow();
                    break;
                case WindowType.Death:
                    _windowFactory.CreateDeathWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}