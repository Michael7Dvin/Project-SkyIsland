using Infrastructure.Progress;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.LevelLoading
{
    public struct LevelLoadRequest
    {
        public LevelLoadRequest(SceneID sceneID, AllProgress progress)
        {
            SceneID = sceneID;
            Progress = progress;
        }

        public SceneID SceneID { get; }
        public AllProgress Progress { get; }
    }
}