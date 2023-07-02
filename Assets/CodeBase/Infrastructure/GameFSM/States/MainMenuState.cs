using Common.FSM;
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
        
        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IWindowsOperator windowOperator)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowOperator = windowOperator;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(SceneType.MainMenu);
            await _uiFactory.RecreateSceneUIObjects();
            await _windowOperator.OpenWindow(WindowType.MainMenu);
        }

        public void Exit()
        {
        }
    }
}