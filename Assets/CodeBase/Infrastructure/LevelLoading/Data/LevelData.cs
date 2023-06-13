namespace Infrastructure.LevelLoading.Data
{
    public class LevelData
    {
        public LevelData(LevelType type, string sceneName)
        {
            Type = type;
            SceneName = sceneName;
        }

        public LevelType Type { get; }
        public string SceneName { get; }
    }
}