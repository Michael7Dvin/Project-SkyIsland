using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services
{
    public class UtilityDataProvider
    {
        public readonly ScenesInfo ScenesInfo;

        public UtilityDataProvider(ScenesInfo scenesInfo)
        {
            ScenesInfo = scenesInfo;
        }
    }
}