using Common.FSM;
using Infrastructure.Services.Input;
using Infrastructure.Services.ResourcesLoading;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.UI;
using UI.Services.Operating;
using UI.Windows;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowsOperator _windowOperator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IInputService _inputService;

        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IWindowsOperator windowOperator,
            IAddressablesLoader addressablesLoader,
            IInputService inputService)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowOperator = windowOperator;
            _addressablesLoader = addressablesLoader;
            _inputService = inputService;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneType.MainMenu);
            await _uiFactory.RecreateSceneUIObjects();
            await _windowOperator.OpenWindow(WindowType.MainMenu);
            
            _inputService.UI.Enable();
        }

        public void Exit()
        {
            _inputService.UI.Disable();
            _addressablesLoader.ClearCache();
        }
    }
}