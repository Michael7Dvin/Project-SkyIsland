using Gameplay.Hero;
using Gameplay.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllAssetsAddresses allAssetsAddresses,
            ScenesData scenesData,
            HeroConfig heroConfig,
            PlayerCameraConfig playerCameraConfig,
            AllUIConfigs allUIConfigs)
        {
            AllAssetsAddresses = allAssetsAddresses;
            ScenesData = scenesData;

            HeroConfig = heroConfig;
            PlayerCameraConfig = playerCameraConfig;
            AllUIConfigs = allUIConfigs;
        }

        public AllAssetsAddresses AllAssetsAddresses { get; }
        public ScenesData ScenesData { get; }

        public HeroConfig HeroConfig { get; }
        public AllUIConfigs AllUIConfigs { get; }
        public PlayerCameraConfig PlayerCameraConfig { get; }
    }
}