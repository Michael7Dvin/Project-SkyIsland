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
        private readonly IWindowFactory _windowFactory;
        
        public MainMenuState(ISceneLoader sceneLoader,
            IWindowFactory windowFactory,
            IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _windowFactory = windowFactory;
            
            _scenesData = staticDataProvider.GetScenesData();
        }

        public void Enter() => 
            _sceneLoader.Load(_scenesData.MainMenuSceneName, OnMainMenuLoaded);

        public void Exit()
        {
        }

        public void OnMainMenuLoaded() => 
            _windowFactory.CreateMainMenuWindow();
    }
}