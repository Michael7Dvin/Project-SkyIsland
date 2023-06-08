using Gameplay.Player;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ScenesData _scenesData;
        private readonly UIConfig _uiConfig;

        public StaticDataProvider(PlayerConfig playerConfig, UIConfig uiConfig, ScenesData scenesData)
        {
            _playerConfig = playerConfig;
            _uiConfig = uiConfig;
            _scenesData = scenesData;
        }

        public PlayerConfig GetPlayerConfig() =>
            _playerConfig;

        public UIConfig GetUIConfig() =>
            _uiConfig;

        public ScenesData GetScenesData() => 
            _scenesData;
    }
}