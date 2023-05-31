using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.UtilityDataProviding
{
    public interface IUtilityDataProvider
    {
        ScenesInfo ScenesInfo { get; }
    }
}