using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.Providers.UI.All
{
    public interface IUiUtilitiesAssetsProvider
    {
        UniTask<Canvas> LoadCanvas();
        UniTask<EventSystem> LoadEventSystem();
    }
}