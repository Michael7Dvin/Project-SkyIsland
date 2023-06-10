using Infrastructure.Services.Input.Service;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Mediating;
using UI.Windows.Factory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly UIConfig _config;
        
        private readonly IWindowFactory _windowFactory;
        private readonly IMediator _mediator;
        private readonly IInstantiator _instantiator;
        private readonly IInputService _inputService;

        public UIFactory(IWindowFactory windowFactory,
            IMediator mediator,
            IInstantiator instantiator,
            IInputService inputService,
            IStaticDataProvider staticDataProvider)
        {
            _windowFactory = windowFactory;
            _mediator = mediator;
            _instantiator = instantiator;
            _inputService = inputService;

            _config = staticDataProvider.GetUIConfig();
        }

        public void Init() => 
            _windowFactory.Init(_mediator);

        public void RecreateSceneUIObjects()
        {
            Canvas canvas = CreateCanvas();
            EventSystem eventSystem = CreateEventSystem();
            SetUpEventSystemInput(eventSystem);
            
            _windowFactory.ResetCanvas(canvas);
        }
        
        private Canvas CreateCanvas()
        {
            Canvas canvas = _instantiator.Instantiate(_config.CanvasPrefab);
            return canvas;
        }
        
        private EventSystem CreateEventSystem()
        {
            EventSystem eventSystem = _instantiator.Instantiate(_config.UIInputEventSystem);
            return eventSystem;
        }

        private void SetUpEventSystemInput(EventSystem eventSystem)
        {
            InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            inputModule.actionsAsset = _inputService.InputActionAsset;
        }
    }
}