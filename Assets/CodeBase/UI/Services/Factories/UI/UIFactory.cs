using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Providers.UI.All;
using Infrastructure.Services.Input;
using Infrastructure.Services.Instantiating;
using UI.Services.Factories.Background;
using UI.Services.Factories.HUD;
using UI.Services.Factories.Window;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace UI.Services.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IBackgroundFactory _backgroundFactory;
        private readonly IHUDFactory _hudFactory;
        private readonly IUIAssetsProvider _uiAssetsProvider;
        
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;

        public UIFactory(IWindowFactory windowFactory,
            IBackgroundFactory backgroundFactory,
            IHUDFactory hudFactory,
            IUIAssetsProvider uiAssetsProvider,
            IInstantiator instantiator,
            IInputService inputService)
        {
            _windowFactory = windowFactory;
            _backgroundFactory = backgroundFactory;
            _hudFactory = hudFactory;
            
            _uiAssetsProvider = uiAssetsProvider;
            _instantiator = instantiator;
            _inputService = inputService;
        }
        
        public async UniTask WarmUp()
        {
            await _uiAssetsProvider.LoadCanvas();
            await _uiAssetsProvider.LoadEventSystem();
        }

        public async UniTask RecreateSceneUIObjects()
        {
            Canvas canvas = await CreateCanvas();
            await CreateEventSystem();

            _windowFactory.ResetCanvas(canvas);
            _backgroundFactory.ResetCanvas(canvas);
            _hudFactory.ResetCanvas(canvas);
        }
        
        private async UniTask<Canvas> CreateCanvas()
        {
            Canvas prefab = await _uiAssetsProvider.LoadCanvas();
            Canvas canvas = _instantiator.InstantiatePrefabForComponent(prefab);
            return canvas;
        }
        
        private async UniTask<EventSystem> CreateEventSystem()
        {
            EventSystem prefab = await _uiAssetsProvider.LoadEventSystem();
            EventSystem eventSystem = _instantiator.InstantiatePrefabForComponent(prefab);
            
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