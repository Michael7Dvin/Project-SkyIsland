using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Services.BackgroundFactory
{
    public interface IBackgroundFactory
    {
        UniTask WarmUp();
        void ResetCanvas(Canvas canvas);
        
        UniTask<GameObject> CreateMainMenu();
        UniTask<GameObject> CreatePause();
        UniTask<GameObject> CreateDeath();
    }
}