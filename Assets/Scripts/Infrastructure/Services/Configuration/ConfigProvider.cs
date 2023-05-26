using Gameplay.Movement;
using Gameplay.Player;

namespace Infrastructure.Services.Configuration
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly MovementConfig _movementConfig;
        private readonly PlayerConfig _playerConfig;

        public ConfigProvider(MovementConfig movementConfig, PlayerConfig playerConfig)
        {
            _movementConfig = movementConfig;
            _playerConfig = playerConfig;
        }

        public MovementConfig GetForMovement() =>
            _movementConfig;
        
        public PlayerConfig GetForPlayer() =>
            _playerConfig;
    }
}