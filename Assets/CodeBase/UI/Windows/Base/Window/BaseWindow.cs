using System;
using Common.Observable;
using UI.Windows.Base.WindowView;

namespace UI.Windows.Base.Window
{
    public abstract class BaseWindow : IWindow
    {
        private readonly Observable<bool> _isOpen = new();
        
        private readonly IWindowView _view;

        protected BaseWindow(IWindowView view)
        {
            _view = view;
            Destroyed += OnDestroy;
        }

        public abstract WindowType Type { get; }
        public IReadOnlyObservable<bool> IsOpen => _isOpen;
        
        public event Action Destroyed
        {
            add => _view.Destroyed += value;
            remove => _view.Destroyed -= value;
        }
        
        public void Open()
        {
            _view.Open();
            SubscribeLogic();
            _isOpen.Value = true;
        }

        public void Close()
        {
            _view.Close();
            UnSubscribeLogic();
            _isOpen.Value = false;
        }

        protected abstract void SubscribeLogic();
        protected abstract void UnSubscribeLogic();
        
        private void OnDestroy()
        {
            Destroyed -= OnDestroy;
            UnSubscribeLogic();
            _isOpen.Value = false;
        }
    }
}