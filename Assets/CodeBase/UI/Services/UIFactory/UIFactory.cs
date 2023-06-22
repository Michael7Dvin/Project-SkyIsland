using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.UI;
using Infrastructure.Services.AssetProviding.UI.All;
using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Instantiating;
using UI.HUD.Factory;
using UI.Services.BackgroundFactory;
using UI.Services.Mediating;
using UI.Windows.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace UI.Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IBackgroundFactory _backgroundFactory;
        private readonly IHUDFactory _hudFactory;
        private readonly IUIAssetsProvider _uiAssetsProvider;
        
        private readonly IMediator _mediator;
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;

        public UIFactory(IWindowFactory windowFactory,
            IBackgroundFactory backgroundFactory,
            IHUDFactory hudFactory,
            IUIAssetsProvider uiAssetsProvider,
            IMediator mediator,
            IInstantiator instantiator,
            IInputService inputService)
        {
            _windowFactory = windowFactory;
            _backgroundFactory = backgroundFactory;
            _hudFactory = hudFactory;
            
            _uiAssetsProvider = uiAssetsProvider;
            _mediator = mediator;
            _instantiator = instantiator;
            _inputService = inputService;
        }

        public void Init() => 
            _windowFactory.Init(_mediator);

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
            Canvas canvas = _instantiator.Instantiate(prefab);
            return canvas;
        }
        
        private async UniTask<EventSystem> CreateEventSystem()
        {
            EventSystem prefab = await _uiAssetsProvider.LoadEventSystem();
            EventSystem eventSystem = _instantiator.Instantiate(prefab);
            
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