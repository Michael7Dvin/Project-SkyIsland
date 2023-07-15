using Infrastructure.LevelLoading.Data;
using Infrastructure.Progress;

namespace Infrastructure.GameFSM.States
{
    public struct LevelLoadingRequest
    {
        public LevelLoadingRequest(LevelData levelData, AllProgress progress)
        {
            LevelData = levelData;
            Progress = progress;
        }

        public LevelData LevelData { get; }
        public AllProgress Progress { get; }
    }
}