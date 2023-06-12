using Gameplay.Hero;
using Gameplay.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        HeroConfig HeroConfig { get; }
        PlayerCameraConfig PlayerCameraConfig { get; }
        ScenesData ScenesData { get; }
        AllAssetsAddresses AllAssetsAddresses { get; }
    }
}