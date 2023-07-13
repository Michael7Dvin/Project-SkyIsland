using System;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Services.LevelLoading.Data
{
    [Serializable]
    public class LevelData
    {
        public LevelType Type;
        public SceneType Scene;

        public LevelData(LevelType type, SceneType scene)
        {
            Type = type;
            Scene = scene;
        }
    }
}