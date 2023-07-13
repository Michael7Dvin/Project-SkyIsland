using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Services.Creation.PlayerCamera
{
    public interface IPlayerCameraFactory
    {
        UniTask WarmUp();
        UniTask<Camera> Create(Transform hero);
    }
}