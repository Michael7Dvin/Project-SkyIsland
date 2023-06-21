using Gameplay.Hero;
using Gameplay.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        
        HeroConfig HeroConfig { get; }
        PlayerCameraConfig PlayerCameraConfig { get; }
        WindowsConfigs WindowsConfigs { get; }
        ScenesData ScenesData { get; }
    }
}