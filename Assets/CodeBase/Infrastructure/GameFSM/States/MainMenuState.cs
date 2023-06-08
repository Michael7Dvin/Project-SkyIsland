using Common.FSM;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Factory;

namespace Infrastructure.GameFSM.States
{
    public class MainMenuState : IState
    {
        private readonly ScenesData _scenesData;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        
        public MainMenuState(ISceneLoader sceneLoader,
            IUIFactory uiFactory,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            
            _scenesData = staticDataProvider.GetScenesData();
        }

        public void Enter() => 
            _sceneLoader.Load(_scenesData.MainMenuSceneName, OnMainMenuLoaded);

        public void Exit()
        {
        }

        public void OnMainMenuLoaded() => 
            _uiFactory.CreateMainMenu();
    }
}