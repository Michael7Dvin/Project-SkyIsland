using Cysharp.Threading.Tasks;
using Gameplay.PlayerCameras;

namespace Gameplay.Services.Spawners.PlayerCameras
{
    public interface IPlayerCameraSpawner
    {
        UniTask<PlayerCamera> Spawn();
    }
}