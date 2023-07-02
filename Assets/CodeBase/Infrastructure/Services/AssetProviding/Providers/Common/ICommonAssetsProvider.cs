using System;
using Cysharp.Threading.Tasks;
using Gameplay.MonoBehaviours.Destroyable;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.Common
{
    public interface ICommonAssetsProvider
    {
        UniTask<GameObject> LoadEmptyGameObject();
        UniTask<Destroyable> LoadHero();
    }
}