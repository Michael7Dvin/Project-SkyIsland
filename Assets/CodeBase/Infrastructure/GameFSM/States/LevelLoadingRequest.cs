using Infrastructure.Progress;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public struct LevelLoadingRequest
    {
        public LevelLoadingRequest(SceneType sceneType, AllProgress progress)
        {
            SceneType = sceneType;
            Progress = progress;
        }

        public SceneType SceneType { get; }
        public AllProgress Progress { get; }
    }
}