using Cysharp.Threading.Tasks;
using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Common
{
    public interface ICommonAssetsProvider
    {
        UniTask<GameObject> LoadEmptyGameObject();
        UniTask<Destroyable> LoadHero();
    }
}