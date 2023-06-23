using System.Collections.Generic;
using System.Linq;
using UI.Windows;
using UI.Windows.Base.Window;

namespace UI.Services.Operating
{
    public class WindowsCache
    {
        private readonly List<IWindow> _cachedWindows = new();

        public void Add(IWindow window)
        {
            window.Destroyed += Remove;
            _cachedWindows.Add(window);

            void Remove()
            {
                window.Destroyed -= Remove;
                _cachedWindows.Remove(window);
            }
        }

        public bool TryGetClosed(WindowType windowType, out IWindow window)
        {
            window = _cachedWindows.FirstOrDefault(window =>
                window.Type == windowType && window.IsOpen.Value == false);
            
            return window != null;
        }
        
        public bool TryGetOpened(WindowType windowType, out IWindow window)
        {
            window = _cachedWindows.FirstOrDefault(window =>
                window.Type == windowType && window.IsOpen.Value == true);
            
            return window != null;
        }

        public int GetCount(WindowType type) => 
            _cachedWindows.Count(window => window.Type == type);
    }
}