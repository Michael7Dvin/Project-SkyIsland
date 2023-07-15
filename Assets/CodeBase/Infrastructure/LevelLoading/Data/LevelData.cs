using System;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.LevelLoading.Data
{
    [Serializable]
    public struct LevelData
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