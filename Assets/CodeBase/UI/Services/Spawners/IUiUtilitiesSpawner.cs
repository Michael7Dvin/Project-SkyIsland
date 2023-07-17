using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Spawners
{
    public interface IUiUtilitiesSpawner
    {
        UniTask<Canvas> SpawnCanvas();
        UniTask<EventSystem> SpawnEventSystem();
    }
}