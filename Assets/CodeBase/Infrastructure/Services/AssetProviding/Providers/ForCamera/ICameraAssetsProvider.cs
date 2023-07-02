using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Providers.ForCamera
{
    public interface ICameraAssetsProvider
    {
        UniTask<Camera> LoadCamera();
        UniTask<CinemachineFreeLook> LoadFreeLookController();
    }
}