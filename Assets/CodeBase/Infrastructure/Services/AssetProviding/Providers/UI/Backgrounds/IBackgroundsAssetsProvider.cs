using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds
{
    public interface IBackgroundsAssetsProvider
    {
        UniTask<GameObject> LoadMainMenu();
        UniTask<GameObject> LoadPause();
        UniTask<GameObject> LoadDeath();
    }
}