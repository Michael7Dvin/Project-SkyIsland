using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.UtilityDataProviding
{
    public class UtilityDataProvider : IUtilityDataProvider
    {
        public UtilityDataProvider(ScenesInfo scenesInfo)
        {
            ScenesInfo = scenesInfo;
        }

        public ScenesInfo ScenesInfo { get; }
    }
}