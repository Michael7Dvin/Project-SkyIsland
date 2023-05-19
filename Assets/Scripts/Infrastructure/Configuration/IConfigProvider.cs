using PlayerCamera;

namespace Infrastructure.Configuration
{
    public interface IConfigProvider
    {
        public PlayerCameraConfig GetForPlayerCamera();
    }
}