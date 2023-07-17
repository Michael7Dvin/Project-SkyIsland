using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Factories.Utilities
{
    public interface IUiUtilitiesFactory
    {
        UniTask WarmUp();
        UniTask<Canvas> CreateCanvas();
        UniTask<EventSystem> CreateEventSystem();
    }
}