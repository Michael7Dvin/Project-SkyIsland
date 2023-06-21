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
            WindowsConfigs windowsConfigs)
        {
            AssetsAddresses = allAssetsAddresses;
            ScenesData = scenesData;

            HeroConfig = heroConfig;
            PlayerCameraConfig = playerCameraConfig;
            WindowsConfigs = windowsConfigs;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public ScenesData ScenesData { get; }

        public HeroConfig HeroConfig { get; }
        public WindowsConfigs WindowsConfigs { get; }
        public PlayerCameraConfig PlayerCameraConfig { get; }
    }
}