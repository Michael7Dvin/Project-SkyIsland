using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.Common
{
    public interface ICommonAssetsProvider
    {
        UniTask<GameObject> LoadEmptyGameObject();
        UniTask<GameObject> LoadHero();
    }
}