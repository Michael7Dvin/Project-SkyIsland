using Gameplay.Player;

namespace Infrastructure.Services.Configuration
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly PlayerConfig _playerConfig;

        public ConfigProvider(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public PlayerConfig GetForPlayer() =>
            _playerConfig;

    }
}