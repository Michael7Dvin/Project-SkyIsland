using Cysharp.Threading.Tasks;
using Gameplay.PlayerCameras;
using UnityEngine;

namespace Gameplay.Services.Creation.PlayerCameras
{
    public interface IPlayerCameraFactory
    {
        UniTask WarmUp();
        UniTask<PlayerCamera> Create(Transform hero);
    }
}