using Cysharp.Threading.Tasks;
using UI.Services.Factories.Utilities;
using UI.Services.Providing.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Spawners
{
    public class UiUtilitiesSpawner : IUiUtilitiesSpawner
    {
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;

        public UiUtilitiesSpawner(IUiUtilitiesFactory uiUtilitiesFactory, IUiUtilitiesProvider uiUtilitiesProvider)
        {
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _uiUtilitiesProvider = uiUtilitiesProvider;
        }

        public async UniTask<Canvas> SpawnCanvas()
        {
            Canvas canvas = await _uiUtilitiesFactory.CreateCanvas();
            _uiUtilitiesProvider.SetCanvas(canvas);
            return canvas;
        }

        public async UniTask<EventSystem> SpawnEventSystem()
        {
            EventSystem eventSystem = await _uiUtilitiesFactory.CreateEventSystem();
            _uiUtilitiesProvider.SetEventSystem(eventSystem);
            return eventSystem;
        }
    }
}