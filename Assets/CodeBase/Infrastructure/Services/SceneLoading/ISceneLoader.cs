using System;

namespace Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onLoaded);
    }
}