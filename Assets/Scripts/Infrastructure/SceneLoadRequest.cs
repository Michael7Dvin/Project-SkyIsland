using System;

namespace Infrastructure
{
    public class SceneLoadRequest
    {
        public SceneLoadRequest(string sceneName, Action onLoad)
        {
            SceneName = sceneName;
            OnLoad = onLoad;
        }

        public string SceneName { get; }
        public Action OnLoad { get; }
    }
}