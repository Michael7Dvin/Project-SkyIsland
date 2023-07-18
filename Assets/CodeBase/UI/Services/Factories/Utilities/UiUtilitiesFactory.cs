using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Providers.UI.All;
using Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace UI.Services.Factories.Utilities
{
    public class UiUtilitiesFactory : IUiUtilitiesFactory
    {
        private readonly IUiUtilitiesAssetsProvider _uiUtilitiesAssetsProvider;
        
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;

        public UiUtilitiesFactory(IUiUtilitiesAssetsProvider uiUtilitiesAssetsProvider,
            IInstantiator instantiator,
            IInputService inputService)
        {
            _uiUtilitiesAssetsProvider = uiUtilitiesAssetsProvider;
            _instantiator = instantiator;
            _inputService = inputService;
        }
        
        public async UniTask WarmUp()
        {
            await _uiUtilitiesAssetsProvider.LoadCanvas();
            await _uiUtilitiesAssetsProvider.LoadEventSystem();
        }
        
        public async UniTask<Canvas> CreateCanvas()
        {
            Canvas prefab = await _uiUtilitiesAssetsProvider.LoadCanvas();
            Canvas canvas = _instantiator.InstantiatePrefabForComponent<Canvas>(prefab);
            return canvas;
        }
        
        public async UniTask<EventSystem> CreateEventSystem()
        {
            EventSystem prefab = await _uiUtilitiesAssetsProvider.LoadEventSystem();
            EventSystem eventSystem = _instantiator.InstantiatePrefabForComponent<EventSystem>(prefab);
            
            SetUpEventSystemInput(eventSystem);

            return eventSystem;
        }

        private void SetUpEventSystemInput(EventSystem eventSystem)
        {
            InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            inputModule.actionsAsset = _inputService.InputActionAsset;
        }
    }
}