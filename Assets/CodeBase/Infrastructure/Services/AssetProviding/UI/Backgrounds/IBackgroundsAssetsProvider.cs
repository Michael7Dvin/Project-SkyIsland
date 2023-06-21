using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.UI.Backgrounds
{
    public interface IBackgroundsAssetsProvider
    {
        UniTask<GameObject> LoadMainMenu();
        UniTask<GameObject> LoadPause();
        UniTask<GameObject> LoadDeath();
    }
}