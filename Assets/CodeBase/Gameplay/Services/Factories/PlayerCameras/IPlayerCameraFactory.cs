using Cysharp.Threading.Tasks;
using Gameplay.PlayerCameras;

namespace Gameplay.Services.Factories.PlayerCameras
{
    public interface IPlayerCameraFactory
    {
        UniTask WarmUp();
        UniTask<PlayerCamera> Create();
    }
}