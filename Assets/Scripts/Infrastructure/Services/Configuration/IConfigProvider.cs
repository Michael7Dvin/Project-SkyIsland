using Gameplay.PlayerCamera;

namespace Infrastructure.Services.Configuration
{
    public interface IConfigProvider
    {
        PlayerCameraConfig GetForPlayerCamera();
    }
}