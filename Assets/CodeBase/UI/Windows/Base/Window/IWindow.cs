using System;
using Common.Observable;

namespace UI.Windows.Base.Window
{
    public interface IWindow
    {
        WindowType Type { get; }
        IReadOnlyObservable<bool> IsOpen { get; }
        
        event Action Destroyed;

        void Open();
        void Close();
    }
}