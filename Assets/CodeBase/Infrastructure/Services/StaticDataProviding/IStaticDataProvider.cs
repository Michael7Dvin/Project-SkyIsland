using Gameplay.Hero;
using Gameplay.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AllAssetsAddresses { get; }
        
        HeroConfig HeroConfig { get; }
        PlayerCameraConfig PlayerCameraConfig { get; }
        AllUIConfigs AllUIConfigs { get; }
        ScenesData ScenesData { get; }
    }
}