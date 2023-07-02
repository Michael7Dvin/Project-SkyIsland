using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.LevelLoading.Data
{
    public class LevelData
    {
        public LevelData(LevelType type, SceneType scene)
        {
            Type = type;
            Scene = scene;
        }

        public LevelType Type { get; }
        public SceneType Scene { get; }
    }
}