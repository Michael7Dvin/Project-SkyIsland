using PlayerCamera;

namespace Infrastructure.Services.Configuration
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly PlayerCameraConfig _playerCameraConfig;

        public ConfigProvider(PlayerCameraConfig playerCameraConfig)
        {
            _playerCameraConfig = playerCameraConfig;
        }

        public PlayerCameraConfig GetForPlayerCamera() =>
            _playerCameraConfig;
    }
}