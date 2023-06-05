using Gameplay.Player;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ScenesData _scenesData;

        public StaticDataProvider(PlayerConfig playerConfig, ScenesData scenesData)
        {
            _playerConfig = playerConfig;
            _scenesData = scenesData;
        }

        public PlayerConfig GetPlayerConfig() =>
            _playerConfig;

        public ScenesData GetScenesData() => 
            _scenesData;
    }
}