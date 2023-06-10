using System;

namespace UI.Windows.Base
{
    public interface IWindow
    {
        WindowType Type { get; }

        event Action<IWindow> Closed;
        
        void Close();
    }
}