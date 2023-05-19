using PlayerCamera;

namespace Infrastructure.Configuration
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