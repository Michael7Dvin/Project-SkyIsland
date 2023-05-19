using System;

namespace Infrastructure.SceneLoading
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onLoad);
    }
}