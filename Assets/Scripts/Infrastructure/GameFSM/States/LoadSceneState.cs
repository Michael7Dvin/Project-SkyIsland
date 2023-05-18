using System;
using Infrastructure.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class LoadSceneState : IStateWithArguments<SceneLoadRequest>
    {
        private readonly SceneLoader _sceneLoader;
        
        public LoadSceneState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneLoadRequest request)
        {
            string sceneName = request.SceneName;
            Action onLoad = request.OnLoad;
            
            _sceneLoader.Load(sceneName, onLoad);
        }

        public void Exit()
        {
        }
    }
}