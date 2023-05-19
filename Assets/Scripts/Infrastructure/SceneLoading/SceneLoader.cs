using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        public async void Load(string sceneName, Action onLoad)
        {
            await SceneManager.LoadSceneAsync(sceneName);
            onLoad?.Invoke();
        }
    }
}