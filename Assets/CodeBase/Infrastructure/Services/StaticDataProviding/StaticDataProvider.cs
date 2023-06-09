using Gameplay.Hero;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly HeroConfig _heroConfig;
        private readonly ScenesData _scenesData;
        private readonly UIConfig _uiConfig;

        public StaticDataProvider(HeroConfig heroConfig, UIConfig uiConfig, ScenesData scenesData)
        {
            _heroConfig = heroConfig;
            _uiConfig = uiConfig;
            _scenesData = scenesData;
        }

        public HeroConfig GetPlayerConfig() =>
            _heroConfig;

        public UIConfig GetUIConfig() =>
            _uiConfig;

        public ScenesData GetScenesData() => 
            _scenesData;
    }
}