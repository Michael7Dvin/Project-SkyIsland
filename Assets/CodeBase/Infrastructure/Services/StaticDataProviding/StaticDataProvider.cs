using Gameplay.Services.Factories.Heros;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.Window;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllAssetsAddresses allAssetsAddresses,
            AllScenesData allScenesData,
            HeroConfig heroConfig,
            WindowsConfigs windowsConfigs)
        {
            AssetsAddresses = allAssetsAddresses;
            ScenesData = allScenesData;

            HeroConfig = heroConfig;
            WindowsConfigs = windowsConfigs;
        }

        public AllAssetsAddresses AssetsAddresses { get; }
        public AllScenesData ScenesData { get; }

        public HeroConfig HeroConfig { get; }
        public WindowsConfigs WindowsConfigs { get; }
    }
}