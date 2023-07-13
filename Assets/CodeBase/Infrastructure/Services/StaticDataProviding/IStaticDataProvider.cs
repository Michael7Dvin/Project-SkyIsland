using Gameplay.Services.Creation.Heros;
using Gameplay.Services.Creation.Heros.Factory;
using Gameplay.Services.Creation.PlayerCamera;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI;
using UI.Services.Factories.Window;

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