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
            await _windowService.OpenWindow(WindowType.MainMenu);
        }
    }
}