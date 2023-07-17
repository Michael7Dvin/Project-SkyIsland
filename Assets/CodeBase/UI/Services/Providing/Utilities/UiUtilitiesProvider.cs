using Common.Observable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Providing.Utilities
{
    public class UiUtilitiesProvider : IUiUtilitiesProvider
    {
        private readonly Observable<Canvas> _canvas = new();
        private readonly Observable<EventSystem> _eventSystem = new();

        public IReadOnlyObservable<Canvas> Canvas => _canvas;
        public IReadOnlyObservable<EventSystem> EventSystem => _eventSystem;
        
        public void SetCanvas(Canvas canvas) => 
            _canvas.Value = canvas;

        public void SetEventSystem(EventSystem eventSystem) => 
            _eventSystem.Value = eventSystem;
    }
}