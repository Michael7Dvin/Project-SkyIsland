using Common.FSM;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.UIFactory;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ScenesData _scenesData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowsOperator _windowOperator;
        
        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IWindowsOperator windowOperator,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowOperator = windowOperator;
            
            _scenesData = staticDataProvider.ScenesData;
        }

        public void Enter() => 
            _sceneLoader.Load(_scenesData.MainMenuSceneName, OnMainMenuLoaded);

        public void Exit()
        {
        }

        public async void OnMainMenuLoaded()
        {
            await _uiFactory.RecreateSceneUIObjects();
            await _windowOperator.OpenWindow(WindowType.MainMenu);
        }
    }
}