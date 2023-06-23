using Gameplay.Services.Factories.HeroFactory;
using Gameplay.Services.Factories.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI;
using UI.Services.Factories.Window;

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