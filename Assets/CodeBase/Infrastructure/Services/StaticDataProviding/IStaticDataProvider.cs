using Gameplay.Services.Factories.Heros;
using Infrastructure.Services.AssetProviding.Addresses;
using Infrastructure.Services.SceneLoading;
using UI.Services.Factories.Window;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        AllAssetsAddresses AssetsAddresses { get; }
        AllScenesData ScenesData { get; }
        
        HeroConfig HeroConfig { get; }
        WindowsConfigs WindowsConfigs { get; }
        
    }
}