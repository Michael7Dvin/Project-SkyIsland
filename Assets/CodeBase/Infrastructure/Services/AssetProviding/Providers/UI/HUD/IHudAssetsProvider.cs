using Cysharp.Threading.Tasks;
using UI.HUD;

namespace Infrastructure.Services.AssetProviding.Providers.UI.HUD
{
    public interface IHUDAssetsProvider
    {
        UniTask<HealthBar> LoadHealthBar();
    }
}