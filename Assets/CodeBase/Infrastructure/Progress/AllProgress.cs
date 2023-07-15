using System;
using Infrastructure.LevelLoading;
using Infrastructure.LevelLoading.Data;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.Progress
{
    [Serializable]
    public class AllProgress
    {
        public readonly SaveSlot SaveSlot;
        
        public LevelData CurrentLevel;

        public HeroProgress HeroProgress;
        public IslandWorldProgress IslandWorldProgress;

        public AllProgress(SaveSlot saveSlot)
        {
            SaveSlot = saveSlot;

            CurrentLevel = new LevelData(LevelType.Island, SceneType.Island);
            HeroProgress = new HeroProgress();
            IslandWorldProgress = new IslandWorldProgress();
        }
    }
}