using Common.FSM;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Factory;
using UI.Services.WindowsOperating;
using UI.Windows;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ScenesData _scenesData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowsService _windowService;
        
        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IWindowsService windowService,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowService = windowService;
            
            _scenesData = staticDataProvider.GetScenesData();
        }

        public void Enter() => 
            _sceneLoader.Load(_scenesData.MainMenuSceneName, OnMainMenuLoaded);

        public void Exit()
        {
        }

        public void OnMainMenuLoaded()
        {
            _uiFactory.RecreateSceneUIObjects();
            _windowService.OpenWindow(WindowType.MainMenu);
        }
    }
}