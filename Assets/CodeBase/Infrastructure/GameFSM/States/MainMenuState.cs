using Common.FSM;
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
        
        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IWindowsOperator windowOperator,
            IAddressablesLoader addressablesLoader)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowOperator = windowOperator;
            _addressablesLoader = addressablesLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneType.MainMenu);
            await _uiFactory.RecreateSceneUIObjects();
            await _windowOperator.OpenWindow(WindowType.MainMenu);
        }

        public void Exit() => 
            _addressablesLoader.ClearCache();
    }
}