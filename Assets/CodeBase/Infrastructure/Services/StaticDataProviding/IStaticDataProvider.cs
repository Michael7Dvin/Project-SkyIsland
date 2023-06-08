using Gameplay.Player;
using Infrastructure.Services.SceneLoading;
using UI;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        PlayerConfig GetPlayerConfig();
        UIConfig GetUIConfig();
        ScenesData GetScenesData();
    }
}