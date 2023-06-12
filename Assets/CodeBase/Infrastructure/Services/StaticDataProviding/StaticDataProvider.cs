using Gameplay.Hero;
using Gameplay.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(HeroConfig heroConfig,
            PlayerCameraConfig playerCameraConfig,
            ScenesData scenesData,
            AllAssetsAddresses allAssetsAddresses)
        {
            HeroConfig = heroConfig;
            PlayerCameraConfig = playerCameraConfig;
            ScenesData = scenesData;
            AllAssetsAddresses = allAssetsAddresses;
        }
        
        public HeroConfig HeroConfig { get; }
        public PlayerCameraConfig PlayerCameraConfig { get; }
        public ScenesData ScenesData { get; }
        public AllAssetsAddresses AllAssetsAddresses { get; }
    }
}