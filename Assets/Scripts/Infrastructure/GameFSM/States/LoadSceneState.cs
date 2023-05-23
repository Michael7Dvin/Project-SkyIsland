using System;
using Common.FSM;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class LoadSceneState : IStateWithArguments<SceneLoadRequest>
    {
        private readonly SceneLoader _sceneLoader;
        
        public LoadSceneState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(SceneLoadRequest argument)
        {
            string sceneName = argument.SceneName;
            Action onLoad = argument.OnLoad;
            
            _sceneLoader.Load(sceneName, onLoad);
        }

        public void Exit()
        {
        }
    }
}