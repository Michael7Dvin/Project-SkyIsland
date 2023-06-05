using Gameplay.Player;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.StaticDataProviding
{
    public interface IStaticDataProvider
    {
        PlayerConfig GetPlayerConfig();
        ScenesData GetScenesData();
    }
}