using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        public async void Load(string sceneName, Action onLoaded)
        {
            await SceneManager.LoadSceneAsync(sceneName);
            onLoaded?.Invoke();
        }
    }
}