using System;

namespace UI.Windows.Base
{
    public interface IWindow
    {
        WindowType Type { get; }
        bool IsActive { get; }
        
        event Action<IWindow> Destroyed;

        void Enable();
        void Disable();
    }
}