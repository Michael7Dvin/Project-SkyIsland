using Gameplay.Hero;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        HeroConfig GetPlayerConfig();
        UIConfig GetUIConfig();
        ScenesData GetScenesData();
    }
}