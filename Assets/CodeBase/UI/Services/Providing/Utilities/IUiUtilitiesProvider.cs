using Common.Observable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Providing.Utilities
{
    public interface IUiUtilitiesProvider
    {
        IReadOnlyObservable<Canvas> Canvas { get; }
        IReadOnlyObservable<EventSystem> EventSystem { get; }

        void SetCanvas(Canvas canvas);
        void SetEventSystem(EventSystem eventSystem);
    }
}