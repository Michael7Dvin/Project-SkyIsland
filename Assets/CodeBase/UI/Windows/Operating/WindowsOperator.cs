using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using UI.Windows;
using UI.Windows.Base.Window;
using UI.Windows.Factory;

namespace UI.Services.WindowsOperating
{
    public class WindowsOperator : IWindowsOperator
    {
        private readonly WindowsCache _cache = new();
        
        private readonly IWindowFactory _windowFactory;
        private readonly ICustomLogger _logger;

        public WindowsOperator(ICustomLogger logger, IWindowFactory windowFactory)
        {
            _logger = logger;
            _windowFactory = windowFactory;
        }

        public async UniTask<IWindow> OpenWindow(WindowType type)
        {
            if (TryOpenFromCache(type, out IWindow openedWindow) == true) 
                return openedWindow;

            if (CanOpenAdditional(type) == true)
                return await CreateNewAndOpen(type);

            _logger.LogWarning(
                $"Can't open {nameof(IWindow)} of '{type}'. Max number of opened instances reached.");
            
            return null;
        }

        public void CloseWindow(WindowType type)
        {
            if (_cache.TryGetOpened(type, out IWindow window))
                window.Close();
            else
                _logger.LogWarning($"{nameof(IWindow)} of '{type}' not found");
        }

        private async UniTask<IWindow> CreateNewAndOpen(WindowType type)
        {
            IWindow window = await _windowFactory.Create(type);
            window.Open();
            _cache.Add(window);
            return window;
        }

        private bool CanOpenAdditional(WindowType type)
        {
            if (_cache.TryGetOpened(type, out IWindow window))
            {
                if (window.MaxOpenedInstances <= _cache.GetCount(type))
                    return false;
            }

            return true;
        }

        private bool TryOpenFromCache(WindowType type, out IWindow openedWindow)
        {
            if (_cache.TryGetClosed(type, out openedWindow))
            {
                openedWindow.Open();
                return true;
            }

            return false;
        }
    }
}