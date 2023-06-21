using Cysharp.Threading.Tasks;
using UI.HUD;

namespace Infrastructure.Services.AssetProviding.UI.HUD
{
    public interface IHUDAssetsProvider
    {
        UniTask<HealthBar> LoadHealthBar();
    }
}