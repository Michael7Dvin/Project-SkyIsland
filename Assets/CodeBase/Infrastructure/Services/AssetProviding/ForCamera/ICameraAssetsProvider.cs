using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.ForCamera
{
    public interface ICameraAssetsProvider
    {
        public UniTask<Camera> LoadCamera();
        public UniTask<CinemachineFreeLook> LoadFreeLookController();
    }
}