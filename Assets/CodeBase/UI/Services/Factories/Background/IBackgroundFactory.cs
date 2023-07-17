using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Services.Factories.Background
{
    public interface IBackgroundFactory
    {
        UniTask WarmUp();
        
        UniTask<GameObject> CreateMainMenu();
        UniTask<GameObject> CreatePause();
        UniTask<GameObject> CreateDeath();
    }
}